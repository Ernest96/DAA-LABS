using AnunturiDesktop.DTO;
using AnunturiDesktop.Helpers;
using AnunturiDesktop.Services;
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

namespace AnunturiDesktop.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly RoleService _roleService;
        private readonly IAccountApi _accountApi;

        public RegisterPage()
        {
            InitializeComponent();
            _roleService = new RoleService();
            _accountApi = RestService.For<IAccountApi>(HttpClientFactory.Create(AppEnvironment.ApiBaseUrl));
        }

        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            ShowLoading();

            await TaskHelper.TryWithErrorHandlingAsync(RegisterUser);

            HideLoading();

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await TaskHelper.TryWithErrorHandlingAsync(_roleService.GetRoles);
            RoleList.ItemsSource = result.Value;
        }

        private async Task RegisterUser()
        {
            var user = new RegisterUserDto()
            {
                Email = Email.Text,
                UserName = Username.Text,
                Password = Password.Password,
                ConfirmPassword = ConfirmPassword.Password,
                Role = RoleList.Text
            };

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.ConfirmPassword) ||
                string.IsNullOrWhiteSpace(user.Role))
            {
                MessageBox.Show("Va rog introduceti toate datele");
            }
            else
            {
                if (user.Password == user.ConfirmPassword)
                {
                    await _accountApi.Register(user);
                    MessageBox.Show("Utilizator înregistrat cu succes !");
                    App.ParentWindowRef.ParentFrame.Navigate(new MenuPage());
                }
                else
                {
                    MessageBox.Show("Parolele nu coincid");
                }
            }

        }

        private void ShowLoading()
        {
            submit.IsEnabled = false;
            Loading.Visibility = Visibility.Visible;
        }

        private void HideLoading()
        {
            submit.IsEnabled = true;
            Loading.Visibility = Visibility.Hidden;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new MenuPage());
        }
    }
}
