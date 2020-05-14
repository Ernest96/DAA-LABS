using Anunturi.Mobile.Infrastructure;
using Anunturi.Mobile.Models;
using Anunturi.Mobile.Models.Enumerations;
using Anunturi.Mobile.Resources.Language;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anunturi.Mobile.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private MainMenuItem _selectedItem;
        private ObservableCollection<MainMenuItem> _menuItems;

        public MenuViewModel(
            IAlertService alertService,
            ISettingsService settingsService,
            INavigationService navigationService)
            : base(alertService, settingsService, navigationService)
        {
            LoadMenuItems();
        }

        public ICommand MenuItemTappedCommand => new AsyncCommand(OnMenuItemTapped);

        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _menuItems;
            set => SetAndRaisePropertyChanged(ref _menuItems, value);
        }

        public MainMenuItem SelectedItem
        {
            get => _selectedItem;
            set => SetAndRaisePropertyChanged(ref _selectedItem, value);
        }

        private void LoadMenuItems()
        {
            MenuItems = new ObservableCollection<MainMenuItem>();

            MenuItems.Add(new MainMenuItem
            {
                MenuText = LanguageResources.LogoutText,
                ViewModelToLoad = typeof(LoginViewModel),
                MenuItemType = MenuItemType.Logout,
                IconSource = "logout.png"
            });
        }

        private async Task OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem);

            if (menuItem != null && menuItem.MenuItemType == MenuItemType.Logout)
            {
                _settingsService.RemoveUserData();
            }

            var type = menuItem?.ViewModelToLoad;
            await _navigationService.NavigateToAsync(type);
            SelectedItem = null;
        }
    }
}
