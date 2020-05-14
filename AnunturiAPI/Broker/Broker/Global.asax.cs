using Broker.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Broker
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        static Thread keepAliveThread = new Thread(KeepAlive);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            PushQueueConfig.InitQueue();
            keepAliveThread.Start();
        }

        static void KeepAlive()
        {
            while (true)
            {
                try
                {
                    WebRequest req = WebRequest.Create(BrokerEnvironment.BrokerBaseUrl);
                    var resp = req.GetResponse();
                    Thread.Sleep(60000);
                }
                catch (ThreadAbortException)
                {
                    break;
                }
            }
        }
    }
}
