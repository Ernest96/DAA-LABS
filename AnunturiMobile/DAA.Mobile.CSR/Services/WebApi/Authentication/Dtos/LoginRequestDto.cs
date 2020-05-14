using Refit;

namespace Anunturi.Mobile.Services.WebApi.Authentication.Dtos
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        [AliasAs("grant_type")]
        public string GrantType => "password";
    }
}
