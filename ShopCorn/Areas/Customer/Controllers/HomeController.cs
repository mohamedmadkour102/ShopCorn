using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using ShopCorn.ViewModels;

namespace ShopCorn.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public HomeController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Product = _unitofWork.ProductRepository.GetAll();
            return View(Product);
        }
        public IActionResult Details(int id )
        {
            ShoppingCartVM obj  = new ShoppingCartVM()
            {
                Product = _unitofWork.ProductRepository.GetFirst(X => X.Id == id),
                Count = 1
            };
              
            return View(obj);
        }

    }
}
