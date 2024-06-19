using Application.Common.ExtensionMethods;
using AspNetCoreHero.ToastNotification.Abstractions;
using GearShopWeb.Areas.Admin.Controllers;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GearShop.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderController : BaseController
{
    #region Constructor

    public OrderController(IUnitOfWork unitOfWork,
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

    public INotyfService _notyfService { get; }

    // GET: OrderController
    public async Task<ActionResult> Index(int? pageNumber)
    {
        var orders = await _db.Order
            .Include(x => x.OrderDetails)
            .AsNoTracking()
            .PaginatedListAsync(pageNumber ?? 1, 4);
        return View(orders);
    }

    // GET: OrderController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: OrderController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: OrderController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: OrderController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: OrderController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: OrderController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: OrderController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    #region Readonlys

    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    #endregion
}