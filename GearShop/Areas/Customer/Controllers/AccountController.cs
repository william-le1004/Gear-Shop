using Application.Common.ExtensionMethods;
using AspNetCoreHero.ToastNotification.Abstractions;
using Infrastructure.Interface;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GearShopWeb.Areas.Admin.Controllers;

[Area("Customer")]
public class AccountController : Controller
{

    #region Readonlys

    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _contxt;
    private readonly IAccountService _accountService;

    #endregion

    public INotyfService _notyfService { get; }

    #region Constructor

    public AccountController(IUnitOfWork unitOfWork,
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

    // GET: LoginController
    public async Task<IActionResult> Login() => View();
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password, string url)
    {
        var account = _accountService.Login(username, password);

        if (account != null)
        {
            _contxt.HttpContext.Session.SetString("User", username);
            _contxt.HttpContext.Session.SetString("Admin", account.Role.RoleName.ToString());
            _contxt.HttpContext.Session.SetString("ID", account.Id.ToString());
            _notyfService.Success("Login Successfully");
            if (url != "noneUser") return RedirectToAction("Index", "Home");
            return RedirectToAction("CheckOut", "Cart");
        }
        else
        {
            ModelState.AddModelError("", "Account does not exist");
            _notyfService.Error("Your information is wrong");
            return View();
        }
    }

    public async Task<IActionResult> SignUp() => View();
    [HttpPost]
    public async Task<IActionResult> SignUp(string name, string username, string email, string password)
    {
        if (ModelState.IsValid)
        {
            _accountService.SignUp(name, username, email, password);
            _notyfService.Success("Sign-up successfully. Now you are able to login !!");
            return RedirectToAction("Login", "Account");
        }
        return View();

    }

    // GET: LoginController1/Details/5
    public async Task<IActionResult> Logout()
    {
        _contxt.HttpContext.Session.Remove("User");
        _contxt.HttpContext.Session.Remove("Admin");
        _contxt.HttpContext.Session.Remove("ID");
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Details(int? id)
    {
        var user = await _unitOfWork.User.Get(x => x.Id == id);
        return View(user);
    }

    public async Task<IActionResult> MyOrder(int? pageNumber)
    {
        var userIdString = _contxt.HttpContext.Session.GetString("ID");
        if (!int.TryParse(userIdString, out int userId))
        {
            // Handle the case where the session string is not a valid integer
            Console.WriteLine("Fail");
        }

        var userOrders = await _db.Order
            .Where(x => x.UserID == userId)
            .PaginatedListAsync(pageNumber ?? 1, 4);
        return View(userOrders);
    }

    public async Task<IActionResult> MyOrderAjax(int? pageNumber)
    {
        var userIdString = _contxt.HttpContext.Session.GetString("ID");
        if (!int.TryParse(userIdString, out int userId))
        {
            // Handle the case where the session string is not a valid integer
            Console.WriteLine("Failed to parse userId from the session string.");
            return Json(new { status = 400, message = "Invalid user ID" });
        }

        var userOrders = await _db.Order
            .Where(x => x.UserID == userId).OrderByDescending(x=>x.Id).PaginatedListAsync(pageNumber ?? 1, 4); ;
        var total = userOrders.TotalPages;
        return Json(new
        {
            code = 200,
            message = "Success",
            orderList = userOrders ,
            toTalPages = total
        });
    }


}

//var userOrders = await _db.Order
//    .Where(x => x.UserID == userId)
//    .Include(x => x.User)
//    .Include(x => x.OrderDetails)
//        .ThenInclude(od => od.Product)
//        .PaginatedListAsync(pageNumber ?? 1, 4);