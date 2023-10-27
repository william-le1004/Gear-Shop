using Application.Common.ExtensionMethods;
using Domain.Entities;
using GearShopWeb.ViewModels;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GearShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        public ProductController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
        // GET: ProductController
        // Using Plug-in X.PagedList
        public async Task<ActionResult> Index(int? pageNumber)
        {
            var productList = await _db.Products.AsNoTracking()
                .Select(x => new HomeProductVM { Name = x.Name, Price = x.Price, ImgUrl = x.ImgUrl, Id = x.Id })
                .PaginatedListAsync(pageNumber ?? 1 , 6);
            return View(productList);
        }
        public ActionResult DoFilter()
        {
            var products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        public async Task<ActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category");
            return View(product);
        }
    }
}
