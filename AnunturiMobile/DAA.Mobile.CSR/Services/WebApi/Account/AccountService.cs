using System.Threading.Tasks;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;

namespace Anunturi.Mobile.Services.WebApi.Account
{
    public class AccountService : IAccountService
    {
        private readonly IRestPoolService _restPoolService;

        public AccountService(IRestPoolService restPoolService)
        {
            _restPoolService = restPoolService;
        }

        public Task<UserInfoDto> GetUserInfo()
        {
            return _restPoolService.AccountApi.GetUserInfo();
        }
    }
}
