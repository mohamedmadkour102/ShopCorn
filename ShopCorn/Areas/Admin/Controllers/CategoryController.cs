using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using ShopCorn.DAL.Data;

namespace ShopCorn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _env;

        public CategoryController(IUnitofWork unitofWork, IWebHostEnvironment env)
        {

            _unitofWork = unitofWork;
            _env = env;
        }
        public ActionResult Index()
        {
            var Categories = _unitofWork.CategoryRepository.GetAll();
            return View(Categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //_dbContext.Categories.Add(category);
                //_dbContext.SaveChanges();
                _unitofWork.CategoryRepository.Add(category);
                _unitofWork.Complete();
                TempData["Create"] = "Data Has been Created Succesfully";
                return RedirectToAction("Index");
            }
            return View(category);

        }
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id.HasValue)
            {
                BadRequest();
            }
            //var Category = _dbContext.Categories.Find(Id);
            var Category = _unitofWork.CategoryRepository.GetFirst(X => X.Id == Id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            try
            {
                //_dbContext.Categories.Update(category);
                //_dbContext.SaveChanges();
                _unitofWork.CategoryRepository.Update(category);
                _unitofWork.Complete();
                TempData["Update"] = "Data Has been Updated Succesfully";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occured during Update Category");

                return View(category);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            //var Category = _dbContext.Categories.Find(Id);
            var Category = _unitofWork.CategoryRepository.GetFirst(X => X.Id == Id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {

            try
            {
                //_dbContext.Categories.Remove(category);
                //_dbContext.SaveChanges();
                _unitofWork.CategoryRepository.Remove(category);
                _unitofWork.Complete();
                TempData["Delete"] = "Data Has been Deleted Succesfully";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);


                else
                    ModelState.AddModelError(string.Empty, "An Error Occured during Delete Category");

                return View(category);
            }

        }
    }
}
