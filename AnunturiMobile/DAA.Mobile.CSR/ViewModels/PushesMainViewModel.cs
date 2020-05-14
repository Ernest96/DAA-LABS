using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using System.Threading.Tasks;

namespace Anunturi.Mobile.ViewModels
{
    public class PushesMainViewModel : BaseViewModel
    {
        private UserInfoViewModel _userInfoViewModel;
        private PushesInfoViewModel _pushesInfoViewModel;


        public PushesMainViewModel(
            IAlertService alertService,
            ISettingsService settingsService,
            INavigationService navigationService,
            UserInfoViewModel userInfoViewModel,
            PushesInfoViewModel pushesInfoViewModel)
            : base(alertService, settingsService, navigationService)
        {
            _userInfoViewModel = userInfoViewModel;
            _pushesInfoViewModel = pushesInfoViewModel;
        }

        public UserInfoViewModel UserInfoViewModel
        {
            get => _userInfoViewModel;
            set => SetAndRaisePropertyChanged(ref _userInfoViewModel, value);
        }

        public PushesInfoViewModel PushesInfoViewModel
        {
            get => _pushesInfoViewModel;
            set => SetAndRaisePropertyChanged(ref _pushesInfoViewModel, value);
        }

        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll
            (
                _pushesInfoViewModel.InitializeAsync(data),
                _userInfoViewModel.InitializeAsync(data)
            );
        }
    }
}
