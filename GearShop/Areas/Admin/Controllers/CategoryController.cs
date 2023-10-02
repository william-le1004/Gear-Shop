using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ShoppingWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult CreateAndUpdate(int? id)
        {
            if (id == null || id == 0)
            {
                Category c = new Category();
                return View(c);
            }
            else
            {
                Category c = _unitOfWork.Category.Get(u => u.Id == id);
                return View(c);
            }
        }

        [HttpPost]
        public IActionResult CreateAndUpdate(Category obj)
        {
            if (ModelState.IsValid)
            {
                //Create new
                if (obj.Id == 0)
                {
                    _unitOfWork.Category.Add(obj);

                }
                else
                {
                    _unitOfWork.Category.Update(obj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALL
        public IActionResult GetAll()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return Json(new
            {
                data = categoryList
            });
        }

        public IActionResult Delete(int? id)
        {
            Category category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = "Product Updated Successfully"
            });
        }

        #endregion

    }
}
