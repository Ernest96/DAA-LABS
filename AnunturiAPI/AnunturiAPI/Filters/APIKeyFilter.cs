using System.Web;
using System.Web.Mvc;

namespace AnunturiAPI.Filters
{
    public class APIKeyFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apikey = HttpContext.Current.Request.Headers["api-key"];

            if (apikey != APIEnvironment.ApiKey)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}