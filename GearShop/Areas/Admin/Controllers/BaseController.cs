using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GearShopWeb.Areas.Admin.Controllers;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = HttpContext.Session.GetString("User");
        var role = HttpContext.Session.GetString("Admin");
        if (user == null)
            context.Result = new RedirectToRouteResult
            (
                new RouteValueDictionary
                {
                    { "area", "Customer" },
                    { "controller", "Login" },
                    { "action", "Index" }
                }
            );
        if (role != "Admin")
            context.Result = new RedirectToRouteResult
            (
                new RouteValueDictionary
                {
                    { "area", "Customer" },
                    { "controller", "Home" },
                    { "action", "PageNotFound" }
                }
            );

        base.OnActionExecuting(context);
    }
}