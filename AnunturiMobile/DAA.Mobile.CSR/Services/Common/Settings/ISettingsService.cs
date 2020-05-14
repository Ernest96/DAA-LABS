using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.Common.Settings
{
    public interface ISettingsService
    {
        Task AddOrUpdateItem<T>(string key, T value);

        T GetItem<T>(string key);

        string UserAccessToken { get; set; }

        UserInfoDto UserInfo { get; set; }

        string Language { get; set; }

        void RemoveUserData();
    }
}
