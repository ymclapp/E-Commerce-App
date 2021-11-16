using E_Commerce.Models.Identity;
using E_Commerce.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Components
{
    public class UserInfo : ViewComponent
    {
        IUserService userService;

        public UserInfo ( IUserService userService )
        {
            this.userService = userService;
        }

        //Must have a method named Invoke OR InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserDto user = await userService.GetUser(UserClaimsPrincipal);
            return View(user);
        }
    }
}
