using AspNetCoreHero.ToastNotification.Abstractions;
using GearShopWeb.Areas.Admin.Controllers;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace GearShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        #region Readonlys

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        public INotyfService _notyfService { get; }

        #region Constructor

        public HomeController(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment,
            INotyfService notyfService,
            ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _notyfService = notyfService;
            _db = db;
        }

        #endregion
        public async Task<IActionResult> Index()
        {
            //var productId = 
            ViewBag.Sell = await _db.OrderDetail
                .Include(x => x.Product)
                .AsNoTracking()
                .Take(4).OrderByDescending(x=>x.Id)
                .ToListAsync();

            ViewBag.Stock = await _db.Products.SumAsync(x => x.Stock);
            ViewBag.Revenue = await _db.OrderDetail.SumAsync(x => x.TotalPrice);
            ViewBag.Sold = await _db.OrderDetail.SumAsync(x => x.Quantity);
            return View();
        }
       




    }
}
