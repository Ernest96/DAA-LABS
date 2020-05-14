using AnunturiDesktop.DTO;
using AnunturiDesktop.Helpers;
using AnunturiDesktop.Services;
using AnunturiDesktop.Services.Auth;
using AnunturiDesktop.Services.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnunturiDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IIdentityApi _identityApi;

        public MainWindow()
        {
            InitializeComponent();
            _identityApi = RestService.For<IIdentityApi>(HttpClientFactory.Create(AppEnvironment.ApiBaseUrl));
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            ShowLoading();
            var result = await TaskHelper.TryWithErrorHandlingAsync(LogInAsync);
            HideLoading();
        }

        private async Task LogInAsync()
        {
            var loginDto = new LoginRequestDto()
            {
                Password = passwordTextbox.Password,
                UserName = usernameTextbox.Text
            };

            var response = await _identityApi.GetAccessToken(loginDto);

            if (!string.IsNullOrWhiteSpace(response.AccessToken))
            {
                SettingsService.SetAuthSettings(loginDto.UserName, response.AccessToken);

                var menuWindow = new MenuWindow();
                menuWindow.Show();
                this.Close();
            }
        }

        private void ShowLoading()
        {
            loginButton.IsEnabled = false;
            Loading.Visibility = Visibility.Visible;
        }

        private void HideLoading()
        {
            loginButton.IsEnabled = true;
            Loading.Visibility = Visibility.Hidden;
        }
    }
}
