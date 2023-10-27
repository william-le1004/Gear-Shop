using AspNetCoreHero.ToastNotification.Abstractions;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GearShopWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class CartController : Controller
{
    private const string CartSession = "CartSession";
    #region Readonlys

    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _contxt;
    private readonly IAccountService _accountService;

    #endregion

    public INotyfService _notyfService { get; }

    #region Constructor

    public CartController(IUnitOfWork unitOfWork,
        INotyfService notyfService,
        ApplicationDbContext db,
        IHttpContextAccessor contxt,
        IAccountService accountService)
    {
        _unitOfWork = unitOfWork;
        _notyfService = notyfService;
        _db = db;
        _contxt = contxt;
        _accountService = accountService;
    }
    #endregion
    public IActionResult Index()
    {
        var cartSession = _contxt.HttpContext.Session.GetString("CartSession");
        var list = new List<CartItem>();
        if (cartSession != null)
        {
            list = JsonSerializer.Deserialize<List<CartItem>>(cartSession);
        }
        return View(list);
    }

    public IActionResult AddItem(int productID, int quantity)
    {
        var product = _db.Products.FirstOrDefault(x => x.Id == productID);
        var cartSession = _contxt.HttpContext.Session.GetString("CartSession");
        //long total;
        if (cartSession != null)
        {
            var list = JsonSerializer.Deserialize<List<CartItem>>(cartSession);
            if (list.Exists(x => x.product.Id == productID))
            {
                foreach (var item in list)
                {
                    if (item.product.Id == productID) item.Quantity += quantity;
                    //total = (long)(item.Quantity * item.product.Price);
                }
            }
            else
            {
                var cartDetail = new CartItem();
                cartDetail.product = product;
                cartDetail.Quantity = quantity;
                list.Add(cartDetail);
            }
            ViewBag.Cart = list;
            string cartDetailsJson = JsonSerializer.Serialize(list);
            _contxt.HttpContext.Session.SetString("CartSession", cartDetailsJson);
        }
        else
        {
            var cartDetail = new CartItem();
            cartDetail.product = product;
            cartDetail.Quantity = quantity;
            var list = new List<CartItem>();
            list.Add(cartDetail);
            string cartDetailsJson = JsonSerializer.Serialize(list);
            _contxt.HttpContext.Session.SetString("CartSession", cartDetailsJson);
        }
        _notyfService.Success("Added Successfully");
        return RedirectToAction("Index", "Product");
    }
}
