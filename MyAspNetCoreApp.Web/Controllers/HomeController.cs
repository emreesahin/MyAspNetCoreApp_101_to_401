using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using System.Diagnostics;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.ViewModels;
using AutoMapper;
using MyAspNetCoreApp.Web.Filters;

namespace MyAspNetCoreApp.Web.Controllers
{
    [LogFilter]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        Helper _helper;

        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }


        [Route("/")]
        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            var products = _context.Products.OrderByDescending(x => x.Id)
     .Select(x => new ProductPartialViewModel()
     {
         Id = x.Id,
         Name = x.Name,
         Price = x.Price,
         Stock = x.Stock
     }).ToList();
            ViewBag.ProductListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }

        [CustomExceptionFilter]
        public IActionResult Privacy()
        {
            throw new Exception("Veritabanı ile ilgili bir hata meydana geldi");
            var products = _context.Products.OrderByDescending(x => x.Id)
    .Select(x => new ProductPartialViewModel()
    {
        Id = x.Id,
        Name = x.Name,
        Price = x.Price,
        Stock = x.Stock
    }).ToList();
            ViewBag.ProductListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(errorViewModel);
        }

        public IActionResult Visitor()
        {
            return View();
        }

        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {
                var visitor = _mapper.Map<Visitor>(visitorViewModel);

                _context.Visitors.Add(visitor);

                _context.SaveChanges();

                TempData["result"] = "Yorum Kaydedilmiştir";


                return RedirectToAction(nameof(HomeController.Visitor));

            }
            catch (Exception)
            {
                TempData["result"] = "Yorum Kaydedilirken Bir Hata Meydana Geldi";

                return RedirectToAction(nameof(HomeController.Visitor));
              
            }
         
        }
    }
}
