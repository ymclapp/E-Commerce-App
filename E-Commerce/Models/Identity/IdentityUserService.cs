using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asyncInnApp.Models.Identity
{
    public class IdentityUserService : IUserService  //IdentityUserService in steps, but you can name whatever
    {
        private readonly UserManager<IdentityUser> userManager;

        public IdentityUserService ( UserManager<IdentityUser> userManager )
        {
            this.userManager = userManager;
        }

        public async Task<UserDto> Authenticate ( LoginData data )
        {
            var user = await userManager.FindByNameAsync(data.Username);

            if (!await userManager.CheckPasswordAsync(user, data.Password))//checkpasswordasync returns bool so if we couldn't verify, return null
                return null;

            return new UserDto
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,

            };
        }

        public async Task<UserDto> Register ( RegisterData data, ModelStateDictionary modelState )
        {
            var user = new IdentityUser
            {
                Email = data.Email,
                UserName = data.Username,
                //Password = data.Password,  //NOOOOOOOOOO
            };
            var result = await userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                };

            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }

    }
}
