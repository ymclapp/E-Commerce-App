using E_Commerce.Models;
using E_Commerce.Models.Identity;
using E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDashboardRepository dashboard;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IProductRepository productRepository;

        public AdminController(IDashboardRepository dashboard, IProductCategoryRepository productCategoryRepository, IProductRepository productRepository )
        {
            this.dashboard = dashboard;
            this.productCategoryRepository = productCategoryRepository;
            this.productRepository = productRepository;
        }

        public async Task<IActionResult> Index ( )
        {
            int prodCatCount = await dashboard.GetProductCategoryCount();
            int prodCount = await dashboard.GetProductCount();
            int orderCount = await dashboard.GetPendingOrderCount();

            List<ProductCategory> productCategories = await productCategoryRepository.GetAll();
            //List<Product> products = await productRepository.GetAll();

            var model = new AdminIndexViewModel
            {
                ProductCategoryCount = prodCatCount,
                ProductCategory = productCategories,
                ProductCount = prodCount,
                OrderCount = orderCount,

            };
            return View(model);
            //return View("Index", prodCatName);
        }


    }
}
