using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace asyncInnApp.Models.Identity
{
    public interface IUserService
    {
        Task<UserDto> Register ( RegisterData data, ModelStateDictionary modelState );
        Task<UserDto> Authenticate ( LoginData data );
    }
}
