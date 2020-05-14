using Refit;
using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Account
{
    public interface IAccountApi
    {
        [Get("/api/users/GetUserInfo")]
        Task<UserInfoDto> GetUserInfo();
    }
}
