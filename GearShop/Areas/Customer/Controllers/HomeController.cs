using Domain.Entities;
using GearShopWeb.ViewModels;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GearShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        #region ReadOnly
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var productList = await _db.Products.AsNoTracking()
                .Select(x => new HomeProductVM { Name = x.Name, Price = x.Price, ImgUrl = x.ImgUrl, Id = x.Id })
                .Take(4)
                .ToListAsync();

            ViewBag.newestProduct = await _db.Products.AsNoTracking()
                .Select(x => new HomeProductVM { Name = x.Name, Price = x.Price, ImgUrl = x.ImgUrl, Id = x.Id })
                .OrderByDescending(x => x.Id)
                .Take(4)
                .ToListAsync();

            ViewBag.bestSeller = await _db.Products
                .Join(_db.OrderDetail,
                    product => product.Id,
                    orderDetail => orderDetail.ProductID,
                     (product, orderDetail) => new HomeProductVM
                     {
                         Id = product.Id,
                         Name = product.Name,
                         Price = product.Price,
                         ImgUrl = product.ImgUrl,
                     })
                .GroupBy(item => new { item.Id, item.Name, item.Price, item.ImgUrl })
                .AsNoTracking()
                .Take(4)
                .Select(group => new HomeProductVM
                {
                    Id = group.Key.Id,
                    Name = group.Key.Name,
                    Price = group.Key.Price,
                    ImgUrl = group.Key.ImgUrl,
                })
                .ToListAsync();
            return View(productList);
        }
       
        public IActionResult ContactUs() => View();
        public IActionResult Blog() => View();
        public IActionResult Privacy() => View();
        public IActionResult PageNotFound() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}