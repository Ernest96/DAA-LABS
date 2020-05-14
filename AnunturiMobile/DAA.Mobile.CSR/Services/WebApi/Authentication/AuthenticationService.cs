using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRestPoolService _restPoolService;
        private readonly ISettingsService _settingsService;

        public AuthenticationService(IRestPoolService restPoolService, ISettingsService settingsService)
        {
            _restPoolService = restPoolService;
            _settingsService = settingsService;
        }

        public async Task<LoginResponseDto> LogInAsync(string userName, string password)
        {
            var authenticationRequest = new LoginRequestDto()
            {
                UserName = userName,
                Password = password
            };

            return await _restPoolService.IdentityApi.GetAccessToken(authenticationRequest);
        }

        public void Authenticate(string accessToken)
        {
            _settingsService.UserAccessToken = accessToken;
            _restPoolService.SetRequestsAuthenticationToken(accessToken);  
        }

        public bool IsUserAuthenticated
        {
            get => !string.IsNullOrEmpty(_settingsService.UserAccessToken);
        }
    }
}
