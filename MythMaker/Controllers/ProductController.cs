using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MythMaker.Data;
using MythMaker.Models;

namespace MythMaker.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        private void PopulateCategoryList()
        {
            ViewBag.CategoryList = _db.Categories 
                .Select(u => new SelectListItem
                {
                    Text = u.Name, 
                    Value = u.Id.ToString()
                })
            .ToList();
        }

        public IActionResult Index(int id)
        {
            // Fetch all products and categories
            List<Product> products = _db.Products.ToList();
            List<Category> categories = _db.Categories.ToList();

            // Fetch the current product based on the provided id
            Product currentProduct = _db.Products.FirstOrDefault(c => c.Id == id);

            // Fetch all cart items from the database
            //List<ShoppingCart> cartItems = _db.ShoppingCarts.ToList();

            var viewModel = new ViewModel
            {
                CurrentProduct = currentProduct,
                Products = products,
                Categories = categories
                //CartItems = cartItems
            };

            return View(viewModel);
        }

        public IActionResult List()
        {
            List<Product> products = _db.Products.ToList();

            var viewModel = new ViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            PopulateCategoryList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImagePath = @"\images\products\" + fileName;

                    _db.Products.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Product created sucessfully";
                    return RedirectToAction("List", "Product");
                }
                else
                {
                    TempData["error"] = "Image file does not exist";
                }

            }
            PopulateCategoryList();
            return View();
        }

        public IActionResult Edit(int? id)
        {
            // Ensure a valid ID is passed
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Retrieve the product from the database
            Product? productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            // Return the Edit view with the existing product data
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(int? id, IFormFile? file, Product obj)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            // Ensure a valid ID is passed
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Fetch the existing product from the database
            Product? productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            // If the Name or Description are empty, keep the existing values from the database
            if (string.IsNullOrEmpty(obj.Name))
            {
                obj.Name = productFromDb.Name;  // Retain old Name
            }
            if (string.IsNullOrEmpty(obj.Description))
            {
                obj.Description = productFromDb.Description;  // Retain old Description
            }

            // If a new file is selected, handle the image upload
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\products");

                // If the product already has an image, delete the old one
                if (!string.IsNullOrEmpty(productFromDb.ImagePath))
                {
                    Console.WriteLine($"wwwRootPath: {wwwRootPath}");
                    var oldImagePath = Path.Combine(wwwRootPath, productFromDb.ImagePath.TrimStart('\\'));

                    Console.WriteLine($"Attempting to delete: {oldImagePath}");
                    // Only delete the old image if it exists
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);  // Delete old image
                    }
                    else
                    {
                        Console.WriteLine("File does not exist: " + oldImagePath);
                    }
                }

                // Save the new image file
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Update the product with the new image path
                productFromDb.ImagePath = @"\images\products\" + fileName;
            }

            // Update other fields in the product object (no image update if no new file)
            productFromDb.Name = obj.Name;
            productFromDb.Description = obj.Description;
            productFromDb.Price = obj.Price;
            productFromDb.DisplayOrder = obj.DisplayOrder;

            // Save the changes to the database
            _db.Products.Update(productFromDb);
            _db.SaveChanges();

            // Show success message and redirect to the list
            TempData["success"] = "Product updated successfully";
            return RedirectToAction("List", "Product");
        }


        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Products.Update(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Product updated sucessfully";
        //        return RedirectToAction("List", "Product");
        //    }
        //    return View();
        //}


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();

            }
            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Product deleted sucessfully";
            return RedirectToAction("List", "Product");
        }
    }
}
