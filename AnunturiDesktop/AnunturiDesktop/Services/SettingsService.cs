using AnunturiDesktop.Helpers;
using AnunturiDesktop.Services.Models;

namespace AnunturiDesktop.Services
{
    public static class SettingsService
    {
        public static CurrentUser CurrentUser { get; set; }

        public static void SetAuthSettings(string username, string token)
        {
            CurrentUser = new CurrentUser()
            {
                Token = token,
                Username = username
            };

            HttpClientFactory.SetAuthenticationHeader(token);
        }
    }
}
