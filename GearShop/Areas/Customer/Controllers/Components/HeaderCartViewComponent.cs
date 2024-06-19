using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Areas.Customer.Controllers.Components;

public class HeaderCartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}