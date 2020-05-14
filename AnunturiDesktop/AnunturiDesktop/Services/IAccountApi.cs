using AnunturiDesktop.DTO;
using Refit;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services
{
    public interface IAccountApi
    {
        [Post("/api/Account/Register")]
        Task<object> Register(RegisterUserDto userDto);
    }
}
