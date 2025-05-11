using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythMaker.Data;
using MythMaker.Models;

namespace MythMaker.Controllers
{
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            // Get all cart items (shopping carts) and their related products
            var cartItems = _db.ShoppingCarts.Include(c => c.Product).ToList();  // Eagerly load the related Product

            // Get all products (if needed for other purposes)
            var products = _db.Products.ToList();

            // Create the ViewModel and pass both the cart items and products
            var viewModel = new ViewModel
            {
                CartItems = cartItems,
                Products = products
            };

            return View(viewModel);
        }


        public IActionResult AddToCart(int productId)
        {
            // Retrieve the product from the database
            var product = _db.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                TempData["error"] = "Product not found";
                return RedirectToAction("Index", "Product");
            }

            // Get the current cart from the session or create a new one
            var cart = HttpContext.Session.GetObjectFromJson<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();

            // Check if the product is already in the cart
            var existingCartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (existingCartItem == null)
            {
                // Add the product to the cart if it's not already there
                var cartItem = new ShoppingCart
                {
                    ProductId = productId,
                };
                cart.Add(cartItem);
            }

            // Save the updated cart back to the session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            TempData["success"] = $"{product.Name} added to cart";
            return RedirectToAction("Index", "Product");
        }

        public IActionResult CartSummary()
        {
            var cartItems = _db.ShoppingCarts.ToList();

            // Fetch product details for each item in the cart
            var cartDetails = cartItems.Select(cartItem => new
            {
                Product = _db.Products.FirstOrDefault(p => p.Id == cartItem.ProductId),
                Quantity = 1 // Adjust this if you track quantities in your model
            }).ToList();

            return PartialView("_CartSummary", cartDetails);
        }
    }
}
