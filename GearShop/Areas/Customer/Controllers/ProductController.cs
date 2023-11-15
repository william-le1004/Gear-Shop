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
                 .Include(x=>x.Category)
                 .Where(x => CatID == 0 || x.CategoryId == CatID)
                 .OrderByDescending(x => x.Id)
                 .Select(x => new HomeProductVM
                 {
                     Name = x.Name,
                     Price = x.Price,
                     ImgUrl = x.ImgUrl,
                     Id = x.Id,
                     category = x.Category.Name
                 }).PaginatedListAsync(pageNumber ?? 1, 4);
            return View(query);
        }


        public async Task<ActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category");
            return View(product);
        }
    }
}
