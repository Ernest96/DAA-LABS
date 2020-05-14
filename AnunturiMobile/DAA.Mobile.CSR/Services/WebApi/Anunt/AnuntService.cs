using Anunturi.Mobile.Models;
using Anunturi.Mobile.Services.Common.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Anunt
{
    public class AnuntService : IAnuntService
    {
        private readonly IRestPoolService _restPoolService;
        private readonly ISettingsService _settingsService;

        public AnuntService(IRestPoolService restPoolService, ISettingsService settingsService)
        {
            _restPoolService = restPoolService;
            _settingsService = settingsService;
        }

        public Task<IList<PushInfo>> GetPushInfos()
        {
            var role = _settingsService.UserInfo.Role;
            return _restPoolService.AnuntApi.GetAnunturi(role);
        }

        public Task<PushModel> GetPush(int pushId)
        {
            return _restPoolService.AnuntApi.GetAnunt(pushId);
        }
    }
}
