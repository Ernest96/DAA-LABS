using Anunturi.Mobile.Infrastructure.Constants;
using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Settings;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.SignalR
{
    public class HubService : IHubService
    {
        private HubConnection _hubConnection;
        private CustomHttpClient client = new CustomHttpClient((sender, certificate, chain, sslPolicyErrors) =>
        {
            //put some validation logic here if you want to.
            return true;
        });

        private readonly ISettingsService _settingsService;
        private readonly IAlertService _alertService;

        public HubService(ISettingsService settingsService, IAlertService alertService)
        {
            _alertService = alertService;
            _settingsService = settingsService;
        }

        public bool IsConnected { get; set; }

        public HubConnection GetHubConnection()
        {
            return _hubConnection;
        }

        public async Task StopHub()
        {
            if (IsConnected)
            {
                await Task.Run(() => _hubConnection.Stop());
                _hubConnection = null;
                IsConnected = false;
            }
        }

        public Task InitHub()
        {
            if (!IsConnected)
            {
                _hubConnection = new HubConnection(ApiConstants.AnuntHubUrl);
                _hubConnection.Headers.Add("access_token", _settingsService.UserAccessToken);
                _hubConnection.Headers.Add("api-key", ApiConstants.ApiKey);
            }

            return Task.FromResult(true);
        }

        public async Task StartHub()
        {
            if (!IsConnected)
            {
                try
                {
                    await _hubConnection.Start(client);
                    IsConnected = true;
                }
                catch (Exception e)
                {
                    _alertService.DisplayErrorAlert(e.Message);
                }
            }
        }
    }
}
