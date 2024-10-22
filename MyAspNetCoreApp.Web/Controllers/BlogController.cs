using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Article(string names)
        {

            var routes = Request.RouteValues["article"];

            return View();
        }
    }
}
