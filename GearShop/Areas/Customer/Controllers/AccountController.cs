using AspNetCoreHero.ToastNotification.Abstractions;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(string username,string password)
    {
        var account = _accountService.Login(username, password);

        if (account != null)
        {
            _contxt.HttpContext.Session.SetString("User", username);
            _contxt.HttpContext.Session.SetString("Admin", account.Role.RoleName.ToString());
            _contxt.HttpContext.Session.SetString("ID", account.Id.ToString());
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("", "Account does not exist");
            return View();
        }
    }

    // GET: LoginController1/Details/5
    public async Task<IActionResult> Logout()
    {
        _contxt.HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Details(int? id)
    {
        var user = await _unitOfWork.User.Get(x => x.Id == id);
        return View(user);
    }

    
}
