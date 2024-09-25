using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{

    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class OrnekController : Controller
    {
        public IActionResult Index()
        {

            var productList = new List<Product2>()
            {
                new(){Id = 1, Name="Kalem"},
                new(){Id = 2, Name="Defter"},
                new(){Id = 3, Name="Silgi"}
            };


            return View(productList);
        }

        public IActionResult Index2()
        {
  

            return View();
        }
        public IActionResult Index3()
        {

            // veritabanı kaydetme işlemlerinde kullanılıyor genelde 

            return RedirectToAction("Index");
            //return View();
        }

        public IActionResult ParameterView(int id)
        {
            return RedirectToAction("JsonResultParameter", "Ornek", new { id = id });
        }
        public IActionResult JsonResultParameter(int id)
        {
            return Json(new { Id = id });
        }
        public IActionResult ContentResult()
        {
            return Content("Content Result");
        }

        public IActionResult JsonResult() {
            return Json(new { id = 1, name = "kalem 1", price = 100 });
                }

        public IActionResult EmptyResult()
        {
            return new EmptyResult();
        }
    }
}
