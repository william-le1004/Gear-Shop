using Application.Common.ExtensionMethods;
using Application.Pagination;
using Domain.Entities;
using GearShopWeb.ViewModels;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GearShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        #region ReadOnly
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        #endregion

        public ProductController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
        // GET: ProductController
        public async Task<ActionResult> Index(int? pageNumber, int CatID = 0)
        {
            ViewBag.SelectList = await _db.Categories.AsNoTracking().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToListAsync();

            ViewBag.CurrentFilter = CatID;
            var query = await _db.Products
                 .AsNoTracking()
                 .Where(x => CatID == 0 || x.CategoryId == CatID)
                 .OrderByDescending(x => x.Id)
                 .Select(x => new HomeProductVM
                 {
                     Name = x.Name,
                     Price = x.Price,
                     ImgUrl = x.ImgUrl,
                     Id = x.Id
                 }).PaginatedListAsync(pageNumber ?? 1, 6);
            return View(query);
        }

        [HttpGet]
        //public async Task<IActionResult> GetProductByCategory(int CatID, int pageNumber = 1)
        //{
        //    var query = _db.Products.AsNoTracking()
        //        .Where(x => x.CategoryId == CatID)
        //        .Select(x => new HomeProductVM
        //        {
        //            Name = x.Name,
        //            Price = x.Price,
        //            ImgUrl = x.ImgUrl,
        //            Id = x.Id,
        //            category = _db.Categories.AsNoTracking().ToList()
        //        })
        //        .OrderByDescending(x => x.Id);

        //    var productList = await PaginatedList<HomeProductVM>.CreateAsync(query, pageNumber, 4);

        //    // Pass productList to the corresponding partial view
        //    return PartialView("_ProductListPartial", productList);
        //}


        public ActionResult Filter(int CatID = 0)
        {
            var url = $"/Customer/Product/Index?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Customer/Product";
            }
            //var products = _unitOfWork.Product.GetAll();
            //return View(products);
            return Json(new
            {
                status = "success",
                redirectUrl = url
            });
        }

        public async Task<ActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category");
            return View(product);
        }
    }
}
