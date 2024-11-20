using Microsoft.AspNetCore.Mvc;

namespace ShopCorn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
