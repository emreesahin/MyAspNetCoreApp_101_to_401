using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductController : Controller
    {

        private AppDbContext _context;

        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;

        public ProductController(AppDbContext context, IMapper mapper)
        {

            //DI Container
            _productRepository = new ProductRepository();

            _context = context;
            _mapper = mapper;


            //if (!_context.Products.Any()) {
            //    _context.Products.Add(new Product { Name = "Kalem 1", Price = 100, Stock = 100, Color ="Red"});
            //    _context.Products.Add(new Product { Name = "Kalem 2", Price = 100, Stock = 200, Color = "Red"});
            //    _context.Products.Add(new Product { Name = "Kalem 3", Price = 100, Stock = 300, Color = "Red"});

            //    _context.SaveChanges();
            //}
        }

        public IActionResult Index()
        {    

            var products = _context.Products.ToList();
            return View(_mapper.Map<List<ProductViewModel>>(products));  
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

           

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay", 1 },
                {"3 Ay", 3 },
                {"6 Ay", 6 },
                {"12 Ay", 12 }
            };


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){ Data = "Mavi", Value = "Mavi"},
                new(){ Data = "Kırmızı", Value = "Kırmızı"},
                new(){ Data = "Yeşil", Value = "Yeşil"},
            }, "Value", "Data");



            return View();
        }

        [HttpPost]

        public IActionResult Add(ProductViewModel newProduct)
        {
            //if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("A"))
            //{
            //    ModelState.AddModelError(String.Empty, "Ürün ismi A harfi ile başlayamaz!");
            //}


            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay", 1 },
                {"3 Ay", 3 },
                {"6 Ay", 6 },
                {"12 Ay", 12 }
            };


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){ Data = "Mavi", Value = "Mavi"},
                new(){ Data = "Kırmızı", Value = "Kırmızı"},
                new(){ Data = "Yeşil", Value = "Yeşil"},
            }, "Value", "Data");



            if (ModelState.IsValid)
            {

                try
                {
                    //throw new Exception("Db Hatası");
                    _context.Products.Add(_mapper.Map<Product>(newProduct));
                    _context.SaveChanges();

                    TempData["status"] = "Ürün Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ürün kaydedilirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz");
                    return View();
                }
                
            }
            else
            {

            }
            {



                return View();

            }
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

            ViewBag.radioExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay", 1 },
                {"3 Ay", 3 },
                {"6 Ay", 6 },
                {"12 Ay", 12 }
            };


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){ Data = "Mavi", Value = "Mavi"},
                new(){ Data = "Kırmızı", Value = "Kırmızı"},
                new(){ Data = "Yeşil", Value = "Yeşil"},
            }, "Value", "Data", product.Color);







            return View(_mapper.Map<ProductViewModel>(product));

        }
        [HttpPost]
        public IActionResult Update(ProductViewModel updateProduct)
        {

            if (!ModelState.IsValid)
            {
                

                ViewBag.radioExpireValue = updateProduct.Expire;
                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay", 1 },
                {"3 Ay", 3 },
                {"6 Ay", 6 },
                {"12 Ay", 12 }
            };


                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){ Data = "Mavi", Value = "Mavi"},
                new(){ Data = "Kırmızı", Value = "Kırmızı"},
                new(){ Data = "Yeşil", Value = "Yeşil"},
            }, "Value", "Data", updateProduct.Color);


                return View();
            }

            _context.Products.Update (_mapper.Map<Product>(updateProduct));
            _context.SaveChanges();
            TempData["status"] = "Ürün Başarıyla Güncellendi";

            return RedirectToAction("Index");

          
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName (string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());

            if (anyProduct)
            {
                return Json("Kaydedilmek İstenen Ürün İsmi Veritabanında Bulunmaktadır");
            }

            else
            {
                return Json(true);
            }
        }

    }
}
