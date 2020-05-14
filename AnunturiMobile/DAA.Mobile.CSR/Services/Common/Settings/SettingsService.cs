using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anunturi.Mobile.Services.Common.Settings
{
    public class SettingsService : ISettingsService
    {
        private const string _accessToken = "access_token";
        private const string _language = "language";
        private static UserInfoDto _userInfo;

        private Task AddOrUpdateValue<T>(string key, T value) => AddOrUpdateItem(key, value);

        public async Task AddOrUpdateItem<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;

            await Application.Current.SavePropertiesAsync();
        }

        public T GetItem<T>(string key)
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = Application.Current.Properties[key];
            }
            return (T)value;
        }

        public string UserAccessToken
        {
            get => GetItem<string>(_accessToken);
            set => AddOrUpdateValue(_accessToken, value);
        }

        // We store UserInfoDto as a json because App Properties is not persisting a object properly
        public UserInfoDto UserInfo
        {
            get
            {
                return _userInfo;
            }
            set
            {
                _userInfo = value;
            }
        }

        public string Language
        {
            get => GetItem<string>(_language);
            set => AddOrUpdateValue(_language, value);
        }

        public void RemoveUserData()
        {
            UserInfo = null;
            UserAccessToken = null;
            Language = null;
        }
    }
}
