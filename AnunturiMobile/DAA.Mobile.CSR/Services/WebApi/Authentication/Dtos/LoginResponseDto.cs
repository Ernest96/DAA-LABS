using Newtonsoft.Json;

namespace Anunturi.Mobile.Services.WebApi.Authentication.Dtos
{
    public class LoginResponseDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
