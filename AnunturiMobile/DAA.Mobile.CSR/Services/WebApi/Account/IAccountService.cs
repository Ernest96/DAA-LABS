using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Account
{
    public interface IAccountService
    {
        Task<UserInfoDto> GetUserInfo();
    }
}
