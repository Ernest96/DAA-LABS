using Anunturi.Mobile.Infrastructure;
using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Models;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.SignalR;
using Anunturi.Mobile.Services.WebApi.Anunt;
using Microsoft.AspNet.SignalR.Client;
using Plugin.Connectivity;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anunturi.Mobile.ViewModels
{
    public class PushesInfoViewModel : BaseViewModel
    {
        private readonly IHubService _hubService;
        private readonly IAnuntService _anuntService;

        public PushesInfoViewModel(
            IAlertService alertService,
            ISettingsService settingsService,
            IHubService hubService,
            IAnuntService anuntService,
            INavigationService navigationService)
            : base(alertService, settingsService, navigationService)
        {
            Pushes = new ObservableCollection<PushInfo>();
            _hubService = new HubService(settingsService, alertService);
            _anuntService = anuntService;
        }

        public ICommand PushTappedCommand => new AsyncCommand(OnPushTapped);

        public ObservableCollection<PushInfo> Pushes { get; set; }

        public override Task InitializeAsync(object data)
        {
            return Task.WhenAll(
                TryExecuteWithLoadingIndicatorsAsync(LoadPushes, null),
                StartSignalR(),
                ConnectionCheck()
                );
        }

        private async Task OnPushTapped(object pushTappedEventArgs)
        {
            var push = pushTappedEventArgs as PushInfo;

            if (push != null)
            {
                await _navigationService.NavigateToAsync<PushViewModel>(push);
            }
        }

        private async Task StartSignalR()
        {
            await _hubService.InitHub();

            var hubConnection = _hubService.GetHubConnection();
            var p = hubConnection.CreateHubProxy("AnuntHub");

            p.On<PushInfo>("ReceivePush", ReceivePush);

            await _hubService.StartHub();

            var role = _settingsService.UserInfo.Role;

            await p.Invoke("JoinGroup", role);
        }

        private void ReceivePush(PushInfo pushInfo)
        {
            Pushes.Insert(0, pushInfo);
            AudioHelper.PlayNotification();
        }

        private async Task LoadPushes()
        {
            Pushes.Clear();

            var dbPushes = await _anuntService.GetPushInfos();

            foreach(var dbPush in dbPushes)
            {
                Pushes.Add(new PushInfo()
                {
                    Id = dbPush.Id,
                    Subject = dbPush.Subject
                });
            }
        }

        private Task ConnectionCheck()
        {
            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    await _hubService.StopHub();
                    await LoadPushes();
                    await StartSignalR();
                }
                else
                {
                    await _hubService.StopHub();
                }
            };

            return Task.FromResult(Task.CompletedTask);
        }

    }
}
