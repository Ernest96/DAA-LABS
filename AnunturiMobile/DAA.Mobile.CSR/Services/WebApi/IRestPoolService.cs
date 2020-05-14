using Anunturi.Mobile.Services.Anunt.WebApi;
using Anunturi.Mobile.Services.WebApi.Account;
using Anunturi.Mobile.Services.WebApi.Authentication;
using Anunturi.Mobile.Services.WebApi.Comments;

namespace Anunturi.Mobile.Services.WebApi
{
    public interface IRestPoolService
    {
        IIdentityApi IdentityApi { get; }
        IAccountApi AccountApi { get; }
        IAnuntApi AnuntApi { get; }
        ICommentsApi CommentsApi { get; }

        void SetRequestsAuthenticationToken(string accessToken); 
    }
}
