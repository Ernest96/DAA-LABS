using Anunturi.Mobile.ViewModels;
using System;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.Common.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>(object data = null) where TViewModel : BaseViewModel;
        Task NavigateToAsync(Type viewModelType);
    }
}
