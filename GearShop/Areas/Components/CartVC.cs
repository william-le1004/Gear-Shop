using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Areas.Components;

public class CartVC : Controller
{
    public IActionResult HeaderCart()
    {
        return ViewComponent("HeaderCart");
    }

    public IActionResult NumberCart()
    {
        return ViewComponent("NumberCart");
    }
}