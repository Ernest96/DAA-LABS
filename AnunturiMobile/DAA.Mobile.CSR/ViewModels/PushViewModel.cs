using Anunturi.Mobile.Infrastructure;
using Anunturi.Mobile.Models;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.WebApi.Anunt;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anunturi.Mobile.ViewModels
{
    public class PushViewModel : BaseViewModel
    {
        private readonly IAnuntService _anuntService;
        private HtmlWebViewSource _htmlWebViewSource;
        private string _subject;
        private int _pushId;

        public PushViewModel(
            IAlertService alertService,
            ISettingsService settingsService,
            IAnuntService anuntService,
            INavigationService navigationService)
            : base(alertService, settingsService, navigationService)
        {
            IsBusy = true;

            _anuntService = anuntService;
            _htmlWebViewSource = new HtmlWebViewSource();
        }

        public ICommand ViewCommentsCommand => new AsyncCommand(() => TryExecuteWithLoadingIndicatorsAsync(ViewComments, null));


        public HtmlWebViewSource WebViewSource
        {
            get => _htmlWebViewSource;
            set => SetAndRaisePropertyChanged(ref _htmlWebViewSource, value);
        }

        public string Subject
        {
            get => _subject;
            set => SetAndRaisePropertyChanged(ref _subject, value);
        }

        public int PushId
        {
            get => _pushId;
            set => SetAndRaisePropertyChanged(ref _pushId, value);
        }

        public override async Task InitializeAsync(object data)
        {
            var pushInfo = (PushInfo)data;
            PushId = pushInfo.Id;

            await TryExecuteWithLoadingIndicatorsAsync(GetPush, null);
        }

        private async Task GetPush()
        {
            var push = await _anuntService.GetPush(PushId);

            WebViewSource.Html = push.Content;
            Subject = push.Subject;
            PushId = push.Id;
        }

        private async Task ViewComments()
        {
            await _navigationService.NavigateToAsync<CommentsViewModel>(new CommentsInfo() { PushId = PushId, Subject = Subject });
        }
    }
}
