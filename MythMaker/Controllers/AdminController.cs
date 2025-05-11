using Microsoft.AspNetCore.Mvc;

namespace MythMaker.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
