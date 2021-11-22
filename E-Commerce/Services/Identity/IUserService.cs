using E_Commerce.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Services.Identity
{
    public interface IUserService
    {
        Task<UserDto> Register ( RegisterData data, ModelStateDictionary modelState );
        Task<UserDto> Authenticate ( LoginData data );
        Task<UserDto> GetUser ( ClaimsPrincipal user );
        Task<UserDto> GetCurrentUser ( );
        Task<UserDto> CustomerRegister ( CustomerData data, ModelStateDictionary modelState );
        Task Logout ( );
    }
}
