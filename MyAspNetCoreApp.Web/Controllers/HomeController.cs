﻿using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using System.Diagnostics;
using MyAspNetCoreApp.Web.Helpers;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        Helper _helper;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;  
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
        
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}