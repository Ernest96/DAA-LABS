using Refit;
using Anunturi.Mobile.Infrastructure.Constants;
using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.WebApi.Account;
using Anunturi.Mobile.Services.WebApi.Authentication;
using Anunturi.Mobile.Services.Anunt.WebApi;
using Anunturi.Mobile.Services.WebApi.Comments;

namespace Anunturi.Mobile.Services.WebApi
{
    public class RestPoolService : IRestPoolService
    {
        public IIdentityApi IdentityApi { get; }
        public IAccountApi AccountApi { get; }
        public IAnuntApi AnuntApi { get; }
        public ICommentsApi CommentsApi { get; }

        public RestPoolService(ISettingsService settingsService)
        {
            IdentityApi = RestService.For<IIdentityApi>(HttpClientFactory.Create(ApiConstants.BaseApiUrl));
            AccountApi = RestService.For<IAccountApi>(HttpClientFactory.Create(ApiConstants.BaseApiUrl));
            AnuntApi = RestService.For<IAnuntApi>(HttpClientFactory.Create(ApiConstants.BaseApiUrl));
            CommentsApi = RestService.For<ICommentsApi>(HttpClientFactory.Create(ApiConstants.BaseApiUrl));
        }

        public void SetRequestsAuthenticationToken(string accessToken)
        {
            HttpClientFactory.SetAuthenticationHeader(accessToken);
        }
    }
}
