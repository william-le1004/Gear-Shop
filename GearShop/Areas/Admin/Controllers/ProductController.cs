using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;

namespace GearShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        #region DI
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productList);
        }
        public IActionResult CreateAndUpdate(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                product = new Product()
            };
            if (id == null || id == 0)
            {
                // create 
                return View(productVM);
            }
            else
            {
                //update
                productVM.product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult CreateAndUpdate(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                String wwwRootPath = _webHostEnvironment.WebRootPath;
                if (wwwRootPath != null)
                {
                    String fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    String productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.product.ImgUrl))
                    {
                        //Delete old image
                        //var oldImage = productPath + fileName;
                        var oldImage = Path.Combine(wwwRootPath, productVM.product.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.product.ImgUrl = @"\images\product\" + fileName;
                    //productVM.Product.ImgUrl = fileName;
                }

                if (productVM.product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View();
            }
        }

        #region API CALL
        public IActionResult GetAll()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new
            {
                data = productList
            });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            var oldImage = Path.Combine(_webHostEnvironment.WebRootPath
                            , product.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product Updated Successfully" });
        }
        #endregion
    }
}
