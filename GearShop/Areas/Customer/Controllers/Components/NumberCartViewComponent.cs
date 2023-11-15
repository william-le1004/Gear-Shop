using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GearShopWeb.Areas.Customer.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //var cart = HttpContext.Session.GetString("CartSession");
            //var list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            
            return View();
        }
    }
}
