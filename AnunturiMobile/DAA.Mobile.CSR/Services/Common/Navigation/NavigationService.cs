using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Anunturi.Mobile.AppStart;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.WebApi.Authentication;
using Anunturi.Mobile.ViewModels;
using Anunturi.Mobile.Views;
using Xamarin.Forms;

namespace Anunturi.Mobile.Services.Common.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;


        protected Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService, ISettingsService settingsService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<LoginViewModel>();
        }

        public Task NavigateToAsync<TViewModel>(object data = null) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), data);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreateAndBindPage(viewModelType, parameter);

            if (page is MainView || page is LoginView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;

                if (mainPage.Detail is CustomNavigationPage navigationPage)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }

            await ((BaseViewModel)page.BindingContext).InitializeAsync(parameter);
        }

        private Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = AutofacConfig.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;
            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
    }
}
