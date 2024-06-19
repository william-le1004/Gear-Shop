using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Areas.Customer.Controllers.Components;

public class NumberCartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        //var cart = HttpContext.Session.GetString("CartSession");
        //var list = JsonConvert.DeserializeObject<List<CartItem>>(cart);

        return View();
    }
}