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


        public AdminController(IDashboardRepository dashboard )
        {
            this.dashboard = dashboard;
        }

        public async Task<IActionResult> Index ( )
        {
            int prodCatCount = await dashboard.GetProductCategoryCount();
            int prodCount = await dashboard.GetProductCount();
            int orderCount = await dashboard.GetPendingOrderCount();

            var prodCatName = await dashboard.IProductCategoryRepository.GetAll().ToList();

            var model = new AdminIndexViewModel
            {
                ProductCategoryCount = prodCatCount,
                ProductCount = prodCount,
                OrderCount = orderCount,
          //      ProductCategoryList = prodCatName,
            };
            //return View(model);
            return View("Index", prodCatName);
        }


    }
}
