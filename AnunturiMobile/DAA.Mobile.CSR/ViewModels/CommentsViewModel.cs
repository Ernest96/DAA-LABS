using Anunturi.Mobile.Infrastructure;
using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Models;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Common.Settings;
using Anunturi.Mobile.Services.SignalR;
using Anunturi.Mobile.Services.WebApi.Comments;
using Anunturi.Mobile.Services.WebApi.Comments.Dto;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anunturi.Mobile.ViewModels
{
    public class CommentsViewModel : BaseViewModel
    {
        private readonly ICommentsService _commentsService;
        private readonly IHubService _hubService;
        private int _pushId;
        private string _content;
        private string _subject;


        public CommentsViewModel(
            IAlertService alertService,
            ISettingsService settingsService,
            ICommentsService commentsService,
            INavigationService navigationService)
            : base(alertService, settingsService, navigationService)
        {
            Comments = new ObservableCollection<CommentItem>();
            _commentsService = commentsService;
            _hubService = new HubService(settingsService, alertService);
        }

        public ICommand PostCommentCommand => new AsyncCommand(() => TryExecuteWithLoadingIndicatorsAsync(PostComment, null));
        public ICommand PageDisappearingCommand => new AsyncCommand(() => TryExecuteWithLoadingIndicatorsAsync(PageDissapear, null));


        public int PushId
        {
            get => _pushId;
            set => SetAndRaisePropertyChanged(ref _pushId, value);
        }

        public string Content
        {
            get => _content;
            set => SetAndRaisePropertyChanged(ref _content, value);
        }

        public string Subject
        {
            get =>  _subject;
            set => SetAndRaisePropertyChanged(ref _subject, value);
        }

        public ObservableCollection<CommentItem> Comments { get; set; }

        private async Task PostComment()
        {
            var tempContent = Content;
            Content = "";
            await _commentsService.CreateComment(PushId, tempContent);
        }

        public override async Task InitializeAsync(object data)
        {
            var info = (CommentsInfo)data;
            PushId = info.PushId;
            Subject = info.Subject;

            await Task.WhenAll(
                TryExecuteWithLoadingIndicatorsAsync(GetComments, null),
                StartSignalR()
                );
            
        }

        private async Task GetComments()
        {
            Comments.Clear();
            var commentsDto = await _commentsService.GetComments(PushId);

            foreach(var dto in commentsDto)
            {
                Comments.Add(new CommentItem()
                {
                    Author = dto.Author,
                    Content = dto.Content,
                    Created = dto.Created.ToShortDateString() + " " + dto.Created.ToShortTimeString()
                });
            }
        }

        private async Task StartSignalR()
        {
            await _hubService.InitHub();

            var hubConnection = _hubService.GetHubConnection();
            var proxy = hubConnection.CreateHubProxy("CommentsHub");

            proxy.On<CommentItemDto>("ReceiveComment", ReceiveComment);

            await _hubService.StartHub();

            await proxy.Invoke("JoinGroup", PushId.ToString());
        }

        private void ReceiveComment(CommentItemDto comment)
        {
            Comments.Add(new CommentItem()
            {
                Author = comment.Author,
                Content = comment.Content,
                Created = comment.Created.ToShortDateString() + " " + comment.Created.ToShortTimeString()
            });
            AudioHelper.PlayNotification();
        }

        private async Task PageDissapear()
        {
           await _hubService.StopHub();
        }
    }
}
