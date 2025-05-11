using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythMaker.Data;
using MythMaker.Models;

namespace MythMaker.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
			_db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int id)
        {
            // Retrieve the cart from the session
            //var cart = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("cart");

            // If no cart exists, initialize an empty list
            //if (cart == null)
            //{
            //    cart = new List<ShoppingCart>();
            //}

            // Retrieve categories and the current category based on the provided id
            List<Category> categories = _db.Categories.ToList();
            Category currentCategory = _db.Categories.Include(c => c.Products)
                                                      .FirstOrDefault(c => c.Id == id);

            // Create a ViewModel instance and pass the necessary data, including the cart
            var viewModel = new ViewModel
            {
                CurrentCategory = currentCategory,
                Categories = categories
                //CartItems = cart // Pass the cart data here
            };

            return View(viewModel);
        }
        public IActionResult List()
        {
            List<Category> categories = _db.Categories.ToList();

            var viewModel = new ViewModel
            {
                Categories = categories
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string categorytPath = Path.Combine(wwwRootPath, @"images\categories");

                    using (var fileStream = new FileStream(Path.Combine(categorytPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImagePath = @"\images\categories\" + fileName;

                    _db.Categories.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Category created sucessfully";
                    return RedirectToAction("List", "Category");
                }
                else
                {
                    TempData["error"] = "Image file does not exist";
                }
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Category obj, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            // Ensure a valid ID is passed
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Fetch the existing category from the database
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }


            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string categoryPath = Path.Combine(wwwRootPath, @"images\categories");

                // If the product already has an image, delete the old one
                if (!string.IsNullOrEmpty(categoryFromDb.ImagePath))
                {
                    Console.WriteLine($"wwwRootPath: {wwwRootPath}");
                    var oldImagePath = Path.Combine(wwwRootPath, categoryFromDb.ImagePath.TrimStart('\\'));

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

                using (var fileStream = new FileStream(Path.Combine(categoryPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                categoryFromDb.ImagePath = @"\images\categories\" + fileName;
            }

            categoryFromDb.Name = obj.Name;
            categoryFromDb.DisplayOrder = obj.DisplayOrder;

            // Save the changes to the database
            _db.Categories.Update(categoryFromDb);
            _db.SaveChanges();

            // Show success message and redirect to the list
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("List", "Category");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();

            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted sucessfully";
            return RedirectToAction("List", "Category");
        }
    }
}

