using Microsoft.AspNetCore.Mvc;
using MythMaker.Data;
using MythMaker.Models;
using System.Diagnostics;

namespace MythMaker.Controllers
{
	public class HomeController : Controller
	{

        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

		public IActionResult Index()
		{
            List<Category> categories = _db.Categories.ToList();

            // Fetch all cart items from the database
            List<ShoppingCart> cartItems = _db.ShoppingCarts.ToList();

            var viewModel = new ViewModel
			{
				Categories = categories,

                CartItems = cartItems
            };
            return View(viewModel);
		}

		public IActionResult Privacy()
		{
            List<Category> categories = _db.Categories.ToList();

            var viewModel = new ViewModel
            {
                Categories = categories
            };
            return View(viewModel);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
