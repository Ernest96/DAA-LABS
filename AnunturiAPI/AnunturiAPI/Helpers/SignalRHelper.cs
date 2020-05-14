using AnunturiAPI.Hubs;
using AnunturiAPI.Models;
using BLL.DTO;
using Microsoft.AspNet.SignalR;

namespace AnunturiAPI.Helpers
{
    public class SignalRHelper
    {
        public void NotifyAnunturiForRole(PushInfo pushInfo, string role)
        {
            IHubContext anuntContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<AnuntHub>();
            anuntContext.Clients.Group(role).ReceivePush(pushInfo);
        }

        public void NotifyCommentsForPush(CommentItemDto dto, int pushId)
        {
            IHubContext commentContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<CommentsHub>();
            commentContext.Clients.Group(pushId.ToString()).ReceiveComment(dto);
        }
    }
}