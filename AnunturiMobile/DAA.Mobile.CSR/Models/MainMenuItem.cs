using Anunturi.Mobile.Models.Enumerations;
using System;
using Xamarin.Forms;

namespace Anunturi.Mobile.Models
{
    public class MainMenuItem : BindableObject
    {
        private string _menuText;
        private MenuItemType _menuItemType;
        private Type _viewModelToLoad;
        private string _iconSource;

        public MenuItemType MenuItemType
        {
            get => _menuItemType;
            set
            {
                _menuItemType = value;
                OnPropertyChanged();
            }
        }

        public string MenuText
        {
            get => _menuText;
            set
            {
                _menuText = value;
                OnPropertyChanged();
            }
        }

        public Type ViewModelToLoad
        {
            get => _viewModelToLoad;
            set
            {
                _viewModelToLoad = value;
                OnPropertyChanged();
            }
        }

        public string IconSource
        {
            get => _iconSource;
            set
            {
                _iconSource = value;
                OnPropertyChanged();
            }
        }
    }
}
