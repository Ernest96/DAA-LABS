using AnunturiAPI.Filters;
using System.Web.Mvc;

namespace AnunturiAPI.Controllers
{
    [IPFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
