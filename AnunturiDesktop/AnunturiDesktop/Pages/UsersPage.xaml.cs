using AnunturiDesktop.Helpers;
using AnunturiDesktop.Models;
using AnunturiDesktop.Services;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnunturiDesktop.Pages
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private readonly IUserApi _userApi;
        private string _selectedUserId;

        public UsersPage()
        {
            InitializeComponent();
            _userApi = RestService.For<IUserApi>(HttpClientFactory.Create(AppEnvironment.ApiBaseUrl));
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)UsersList.SelectedItem;
            _selectedUserId = user.UserId;

            await TaskHelper.TryWithErrorHandlingAsync(DeleteUser);
            await TaskHelper.TryWithErrorHandlingAsync(GetUsersAsync);

            UsersList.SelectedItem = null;
            _selectedUserId = null;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new MenuPage());
        }

        private void UsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = null;
            if (e.AddedItems.Count > 0)
            {
                user = (User)e.AddedItems[0];
            }

            if (user != null)
            {
                Delete.IsEnabled = true;
            }
            else
            {
                Delete.IsEnabled = false;
            }

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await TaskHelper.TryWithErrorHandlingAsync(GetUsersAsync);
        }

        private async Task GetUsersAsync()
        {
            var users = await _userApi.GetUsers();
            UsersList.ItemsSource = users;
        }

        private async Task DeleteUser()
        {
            await _userApi.Delete(_selectedUserId);
        }
    }
}
