using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> LogInAsync(string userName, string password);

        void Authenticate(string accessToken);

        bool IsUserAuthenticated { get; }
    }
}
