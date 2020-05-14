using OperationResult;
using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Anunturi.Mobile.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected IAlertService _alertService;
        protected ISettingsService _settingsService;
        protected INavigationService _navigationService;

        protected BaseViewModel(
            IAlertService alertService, 
            ISettingsService settingsService, 
            INavigationService navigationService)
        {
            _alertService = alertService;
            _settingsService = settingsService;
            _navigationService = navigationService;
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetAndRaisePropertyChanged(ref _isBusy, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }

        public async Task<Status> TryExecuteAsync(
            Func<Task> operation,
            Func<Exception, Task<bool>> onError = null) => 
            await TaskHelper.Create()
                .TryWithErrorHandlingAsync(operation, onError);

        public async Task<Status> TryExecuteWithLoadingIndicatorsAsync(
            Func<Task> operation,
            Func<Exception, Task<bool>> onError = null) =>
            await TaskHelper.Create()
                .WhenStarting(() => IsBusy = true)
                .WhenFinished(() => IsBusy = false)
                .TryWithErrorHandlingAsync(operation, onError);

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
