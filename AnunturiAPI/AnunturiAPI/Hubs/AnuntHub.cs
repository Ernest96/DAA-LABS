using AnunturiAPI.Filters;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace AnunturiAPI.Hubs
{
    [Authorize]
    public class AnuntHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public Task JoinGroup(string group)
        {
            return Groups.Add(Context.ConnectionId, group);
        }
    }
}