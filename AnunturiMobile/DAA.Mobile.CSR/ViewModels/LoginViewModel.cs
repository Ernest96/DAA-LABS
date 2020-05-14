using Anunturi.Mobile.Infrastructure;
using Anunturi.Mobile.Infrastructure.Constants;
using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Models;
using Anunturi.Mobile.Resources.Language;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Language;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.WebApi.Account;
using Anunturi.Mobile.Services.WebApi.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anunturi.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountService _accountService;
        private readonly ILanguageService _languageService;

        private string _username;
        private string _password;
        private string _selectedLanguageText;

        public LoginViewModel(
            IAlertService alertService,
            ISettingsService settingsService,
            INavigationService navigationService,
            IAccountService accountService,
            ILanguageService languageService,
            IAuthenticationService authenticationService) 
            : base(alertService, settingsService, navigationService)
        {
            _authenticationService = authenticationService;
            _accountService = accountService;
            _languageService = languageService;
        }

        public ICommand LogInCommand => new AsyncCommand(() => TryExecuteWithLoadingIndicatorsAsync(LogInAsync, LogInErrorHandler));

        public string Username
        {
            get => _username;
            set => SetAndRaisePropertyChangedIfDifferentValues(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetAndRaisePropertyChangedIfDifferentValues(ref _password, value);
        }

        public string SelectedLanguageText
        {
            get => _selectedLanguageText;
            set => SetAndRaisePropertyChangedIfDifferentValues(ref _selectedLanguageText, value);
        }

        public Language SelectedLanguage { set => ChangeLanguageAndReloadView(value); }

        public IList<Language> LanguageList { get => LanguagesConstants.LanguageList; }

        private Task ChangeLanguageAndReloadView(Language language)
        {
            _languageService.SetApplicationLanguage(language.Code);

            if (!IsBusy)
            {
                var loginModel = new LoginModel()
                {
                    Username = Username,
                    Password = Password,
                    Language = language
                };

                return _navigationService.NavigateToAsync<LoginViewModel>(loginModel);
            }

            return Task.CompletedTask;
        }

        private async Task LogInAsync()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                _alertService.DisplayErrorAlert(LanguageResources.EnterUsernameMessage);
            }
            else
            {
                var loginResponseResult = await _authenticationService.LogInAsync(Username, Password);

                if (!string.IsNullOrWhiteSpace(loginResponseResult.AccessToken))
                {
                    _authenticationService.Authenticate(loginResponseResult.AccessToken);

                    _settingsService.UserInfo = await _accountService.GetUserInfo();

                    await _navigationService.NavigateToAsync<MainViewModel>();
                }
                else
                {
                    _alertService.DisplayErrorAlert(LanguageResources.InvalidLoginCredentialsMessage);
                }
            }
        }

        private Task<bool> LogInErrorHandler(Exception exception)
        {
            if (exception.IsApiBadRequestException())
            {
                _alertService.DisplayErrorAlert(LanguageResources.LoginFailedMessage);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public override Task InitializeAsync(object data)
        {
            var loginModel = (LoginModel)data;

            if (loginModel != null)
            {
                SelectedLanguageText = loginModel.Language.Name;
                Username = loginModel.Username;
            }
            else
            {
                SelectedLanguageText = "Select Language / Seleccione el idioma";
            }

            return Task.CompletedTask;
        }
    }
}
