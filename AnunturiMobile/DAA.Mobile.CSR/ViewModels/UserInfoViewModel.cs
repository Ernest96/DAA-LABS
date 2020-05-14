using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Resources.Language;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.WebApi.Account;
using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System;
using System.Threading.Tasks;

namespace Anunturi.Mobile.ViewModels
{
    public class UserInfoViewModel : BaseViewModel
    {
        private UserInfoDto _userInfo;
        private readonly IAccountService _accountService;
        private string _employeeText;
        private string _roleText;

        public UserInfoViewModel(
            IAlertService alertService,
            IAccountService accountService,
            ISettingsService settingsService,
            INavigationService navigationService)
            : base(alertService, settingsService, navigationService)
        {
            _accountService = accountService;
        }

        public string EmployeeText
        {
            get => _employeeText;
            set => SetAndRaisePropertyChangedIfDifferentValues(ref _employeeText, value);
        }

        public string RoleText
        {
            get => _roleText;
            set => SetAndRaisePropertyChangedIfDifferentValues(ref _roleText, value);
        }

        public override async Task InitializeAsync(object data)
        {
            await TryExecuteAsync(() => GetUserInfo(), GetUserInfoErrorHandler);
        }

        private Task GetUserInfo()
        {
            _userInfo = _settingsService.UserInfo;
            EmployeeText = $"{_userInfo.Username}";
            RoleText = $"{_userInfo.Role}";

            return Task.CompletedTask;
        }

        private Task<bool> GetUserInfoErrorHandler(Exception exception)
        {
            if (exception.IsJsonException())
            {
                _alertService.DisplayErrorAlert(LanguageResources.UserInfoLoadingErrorMessage);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
