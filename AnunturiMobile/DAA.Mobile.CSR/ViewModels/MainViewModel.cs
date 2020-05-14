using Anunturi.Mobile.Infrastructure.Constants;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Anunturi.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MenuViewModel _menuViewModel;


        public MainViewModel(
            IAlertService alertService,
            ISettingsService settingsService,
            INavigationService navigationService,
            MenuViewModel menuViewModel)
            : base(alertService,  settingsService, navigationService)
        {
            _menuViewModel = menuViewModel;
        }


        public MenuViewModel MenuViewModel
        {
            get => _menuViewModel;
            set => SetAndRaisePropertyChanged(ref _menuViewModel, value);
        }

        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll
            (
                _menuViewModel.InitializeAsync(data),
                _navigationService.NavigateToAsync<PushesMainViewModel>()
            );
        }
    }
}
