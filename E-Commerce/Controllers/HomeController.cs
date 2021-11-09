using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_Commerce.Models;
using E_Commerce.Services;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IProductRepository productRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository, ILogger<HomeController> logger)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Home!!");

            List<ProductCategory> productCategories = await productCategoryRepository.GetAll();

            List<ProductCategory> newProductCategories = await productCategoryRepository.GetNew(5);

            return View(productCategories);
        }

        [HttpGet("PrivacyPolicy")]
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
