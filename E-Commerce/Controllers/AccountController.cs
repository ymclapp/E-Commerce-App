using E_Commerce.Models.Identity;
using E_Commerce.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController ( IUserService userService )
        {
            this.userService = userService;
        }

        //[Authorize(Roles = "Site Owner")]
        [Authorize]
        public IActionResult Index ( )
        {
            return View();
        }

        //GET Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST Account/Register
        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterAsync ( RegisterData data )
        {

            if(!ModelState.IsValid)
            {
                return View(data);
            }
            await userService.Register(data, this.ModelState);

            if(!ModelState.IsValid)
            {
                return View(data);
            }
            return RedirectToAction(nameof(Index));
        }

        //GET Account/CustomerRegister
        [HttpGet]
        public IActionResult CustomerRegister ( )
        {
            return View();
        }

        //POST Account/Register
        [HttpPost]
        public async Task<ActionResult<UserDto>> CustomerRegisterAsync ( CustomerData data )
        {

            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await userService.CustomerRegister(data, this.ModelState);

            if (!ModelState.IsValid)
            {
                return View(data);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> Login ( LoginData data )
        {
            var user = await userService.Authenticate(data);

            if (user != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(nameof(LoginData.Password), "Email or Password was incorrect.");
            return View(data);
        }

        //Can't access this if you are not signed in
        [Authorize]  //can be put on any controller or action
        [HttpGet("[action]")]
        public async Task<ActionResult<UserDto>> Self ( )//can I get information about myself
        {
            var user = await userService.GetUser(User);

            if (User == null)
                return NotFound();

            return user;
        }
    }
}
