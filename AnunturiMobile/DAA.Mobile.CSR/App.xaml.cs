using Anunturi.Mobile.AppStart;
using Anunturi.Mobile.Services.Common.Language;
using Anunturi.Mobile.Services.Common.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Anunturi.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeApp();
            InitializeLanguage();
            InitializeNavigation();
        }

        private void InitializeApp()
        {
            AutofacConfig.RegisterDependencies();
        }

        private void InitializeLanguage()
        {
            var languageService = AutofacConfig.Resolve<ILanguageService>();
            languageService.LoadLanguageFromSettings();
        }

        private async Task InitializeNavigation()
        {
            var navigationService = AutofacConfig.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
