using Application.Common.ExtensionMethods;
using AspNetCoreHero.ToastNotification.Abstractions;
using Domain.Entities;
using GearShopWeb.Areas.Admin.Controllers;
using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : BaseController
{
    #region Constructor

    public CategoryController(IUnitOfWork unitOfWork,
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

    public async Task<IActionResult> Index(int? pageNumber)
    {
        var paginatedList = await _db.Categories.PaginatedListAsync(pageNumber ?? 1, 4);
        return View(paginatedList);
    }

    public async Task<IActionResult> CreateAndUpdate(int? id)
    {
        if (id == null || id == 0)
        {
            var c = new Category();
            return View(c);
        }
        else
        {
            var c = await _unitOfWork.Category.Get(u => u.Id == id);
            return View(c);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAndUpdate(Category obj)
    {
        if (ModelState.IsValid)
        {
            //Create new
            if (obj.Id == 0)
            {
                await _unitOfWork.Category.Add(obj);
                _notyfService.Success("Category Added Successfully");
            }
            else
            {
                _unitOfWork.Category.Update(obj);
                _notyfService.Success("Category Updated Successfully");
            }

            await _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        var category = await _unitOfWork.Category.Get(x => x.Id == id);
        if (id == null || id == 0) return NotFound();
        await _unitOfWork.Category.Remove(category);
        await _unitOfWork.Save();
        _notyfService.Success("Deleted!");
        return RedirectToAction("Index");
    }

    #region Readonlys

    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    #endregion
}