using System;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Topshelf.Logging;

[assembly: OwinStartup(typeof(SignalRService.Startup))]
namespace SignalRService
{
    public partial class SignalRService : IDisposable
    {
        public static readonly LogWriter Log = HostLogger.Get<SignalRService>();

        public SignalRService()
        {
        }

        public void OnStart(string[] args)
        {
            Log.InfoFormat("SignalRService: In OnStart");

            string url = "http://192.168.1.3";
            WebApp.Start(url);
        }

        public void OnStop()
        {
            Log.InfoFormat("SignalRService: In OnStop");
        }

        public void Dispose()
        {
        }
    }
}