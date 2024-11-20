using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.BLL.Interfaces;
using Shop.DAL.Models;
using ShopCorn.DAL.Data;
using ShopCorn.ViewModels;

namespace ShopCorn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _env;

        public ProductController(IUnitofWork unitofWork, IWebHostEnvironment env)
        {

            _unitofWork = unitofWork;
            _env = env;
        }
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult GetData()
        {
            var Products = _unitofWork.ProductRepository.GetAll(IncludeWord:"Category");
            return Json(new { data = Products });
        }
        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModels productViewModel = new ProductViewModels() 
            {
                product = new Product(),
                CategoryList = _unitofWork.CategoryRepository.GetAll().Select(x=> new SelectListItem { 
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(productViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModels Productvm, IFormFile File)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _env.WebRootPath;
                if (File != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(rootpath, @"Images\Products");
                    var ext = Path.GetExtension(File.FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, filename + ext), FileMode.Create))
                    {
                        File.CopyTo(filestream);
                    }

                    Productvm.product.Image = @"Images\Products" + filename + ext;
                }

                _unitofWork.ProductRepository.Add(Productvm.product);
                _unitofWork.Complete();
                TempData["Create"] = "Data Has been Created Successfully";
                return RedirectToAction("Index");
            }

          
            Productvm.CategoryList = _unitofWork.CategoryRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(Productvm);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id.HasValue)
            {
                BadRequest();
            }


            ProductViewModels productViewModel = new ProductViewModels()
            {
                product = _unitofWork.ProductRepository.GetFirst(X => X.Id == Id),
                CategoryList = _unitofWork.CategoryRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(productViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModels ProductVM , IFormFile? File)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _env.WebRootPath;
                if (File != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(rootpath, @"Images\Products");
                    var ext = Path.GetExtension(File.FileName);

                    if (ProductVM.product.Image != null)
                    {
                        var oldImag = Path.Combine(rootpath , ProductVM.product.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImag))
                        {
                            System.IO.File.Delete(oldImag);
                        }
                    }



                    using (var filestream = new FileStream(Path.Combine(upload, filename + ext), FileMode.Create))
                    {
                        File.CopyTo(filestream);
                    }

                    ProductVM.product.Image = @"Images\Products" + filename + ext;
                }
            }
            try
            {
                //_dbContext.Categories.Update(Product);
                //_dbContext.SaveChanges();
                _unitofWork.ProductRepository.Update(ProductVM.product);
                _unitofWork.Complete();
                TempData["Update"] = "Data Has been Updated Succesfully";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occured during Update Product");

                return View(ProductVM.product);
            }
        }


        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product Product)
        {
            var productindb = _unitofWork.ProductRepository.GetFirst(X=>X.Id == Product.Id);
            if (productindb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            
            
                //_dbContext.Categories.Remove(Product);
                //_dbContext.SaveChanges();
                _unitofWork.ProductRepository.Remove(productindb);

                var oldImag = Path.Combine(_env.WebRootPath , productindb.Image.TrimStart('\\'));
                if (System.IO.File.Exists(oldImag))
                {
                    System.IO.File.Delete(oldImag);
                }
                _unitofWork.Complete();
                return Json(new { success = true, message = "File has been Deleted" });

            


        }
    }
}
