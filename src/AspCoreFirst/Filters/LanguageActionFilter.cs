using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace AspCoreFirst.Filters
{
    public class LanguageActionFilter : ActionFilterAttribute
    {


        public LanguageActionFilter()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isculture = context.RouteData.Values.ContainsKey("culture");
            if (!isculture)
            {
                context.Result = new RedirectResult("en" + context.HttpContext.Request.Path);
                //context.Result =
                //    new RedirectToRouteResult(new RouteValueDictionary()
                //    {
                //        {"culture", "en"},
                //        {"controller", controller},
                //        {"action", action}
                //    });
            }
            base.OnActionExecuting(context);
        }
    }
}