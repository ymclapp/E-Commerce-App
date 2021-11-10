using asyncInnApp.Models.Identity;
using E_Commerce.Models;
using E_Commerce.Services;
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
        private readonly IUserService userService;


        public AdminController(IDashboardRepository dashboard, IUserService userService )
        {
            this.dashboard = dashboard;
            this.userService = userService;
        }
        public async Task<IActionResult> Index ( )
        {
            int prodCatCount = await dashboard.GetProductCategoryCount();
            int prodCount = await dashboard.GetProductCount();
            int orderCount = await dashboard.GetPendingOrderCount();

            var model = new AdminIndexViewModel
            {
                ProductCategoryCount = prodCatCount,
                ProductCount = prodCount,
                OrderCount = orderCount,
            };
            return View(model);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register ( RegisterData data )
        {
            var user = await userService.Register(data, this.ModelState);
            if (user == null)
                return BadRequest(new ValidationProblemDetails(ModelState));

            return user;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login ( LoginData data )
        {
            var user = await userService.Authenticate(data);

            if (user == null)
                return Unauthorized();

            return user;
        }

        //Can't access this if you are not signed in
        [Authorize]  //can be put on any controller or action
        [HttpGet("[action]")]
        public async Task<ActionResult<UserDto>> Self ( )//can I get information about myself
        {
            var user = await userService.GetUser(this.User);

            if (User == null)
                return NotFound();

            return user;
        }
    }
}
