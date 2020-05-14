using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnunturiAPI.Filters
{
    public class IPFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string usersIpAddress = HttpContext.Current.Request.UserHostAddress;

            if (!IpIsValid(usersIpAddress))
            {
                filterContext.Result = new HttpStatusCodeResult(403);
            }

            base.OnActionExecuting(filterContext);
        }

        private bool IpIsValid(string usersIpAddress)
        {
            return APIEnvironment.IpWhitelist.Contains(usersIpAddress);
        }
    }
}