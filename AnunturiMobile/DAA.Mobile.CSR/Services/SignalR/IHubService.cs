using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.SignalR
{
    public interface IHubService
    {
        Task StopHub();

        Task StartHub();

        Task InitHub();

        HubConnection GetHubConnection();
    }
}
