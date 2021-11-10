using E_Commerce.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Services.Identity
{


    public class IdentityUserService : IUserService  //IdentityUserService in steps, but you can name whatever
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;


        public IdentityUserService ( UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor )
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> Authenticate ( LoginData data )
        {
            var user = await userManager.FindByNameAsync(data.Username);
            if (!await userManager.CheckPasswordAsync(user, data.Password))
                return null;

            return await CreateUserDtoAsync(user);
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
                await userManager.AddToRoleAsync(user, "Administrator");
                return await CreateUserDtoAsync(user);
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
        private async Task<UserDto> CreateUserDtoAsync ( IdentityUser user )
        {
            return new UserDto
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName,

                Roles = await userManager.GetRolesAsync(user),
            };
        }

        public async Task<UserDto> GetCurrentUser ( )
        {
            var principal = httpContextAccessor.HttpContext.User;
            return await GetUser(principal);
        }

        public async Task<UserDto> GetUser ( ClaimsPrincipal principal )
        {
            var user = await userManager.GetUserAsync(principal);
            return await CreateUserDtoAsync(user);
        }
    }
}
