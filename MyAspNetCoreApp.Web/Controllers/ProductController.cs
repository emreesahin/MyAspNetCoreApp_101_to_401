using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductController : Controller
    {

        private AppDbContext _context;
        private IHelper _helper;

        private readonly ProductRepository _productRepository;

        public ProductController(AppDbContext context, IHelper helper)
        {

            //DI Container
            _productRepository = new ProductRepository();

            _context = context;
            _helper = helper;

            //if (!_context.Products.Any()) {
            //    _context.Products.Add(new Product { Name = "Kalem 1", Price = 100, Stock = 100, Color ="Red"});
            //    _context.Products.Add(new Product { Name = "Kalem 2", Price = 100, Stock = 200, Color = "Red"});
            //    _context.Products.Add(new Product { Name = "Kalem 3", Price = 100, Stock = 300, Color = "Red"});

            //    _context.SaveChanges();
            //}
        }
        
        public IActionResult Index([FromServices]IHelper helper2)
        {
            var text = "Asp.Net";
            var upperText = _helper.Upper(text);
            var status = _helper.Equals(helper2);

            
            var products = _context.Products.ToList();
            return View(products);  
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(Product newProduct)
        {


            // Request Header-Body


            // 1. Yöntem 

            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            var color = HttpContext.Request.Form["Color"].ToString();

            // 2. Yöntem
            //Product newProduct = new Product() { Name = Name, Price = Price, Color = Color, Stock = Stock };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün Başarıyla Eklendi";
            return RedirectToAction("Index");
        }



        // HTTPGET ile veri alma 

        //[HttpGet]

        //public IActionResult SaveProduct(Product newProduct) 
        //{ 
        //    _context.Products.Add(newProduct);
        //    _context.SaveChanges();
        //    return View();
        //}


        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId, string type)
        {
           updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün Başarıyla Güncellendi";

            return RedirectToAction("Index");
        }
    }
}
