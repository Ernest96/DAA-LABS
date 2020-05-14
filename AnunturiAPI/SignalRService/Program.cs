using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;


namespace SignalRService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.Service<SignalRService>(serviceInstance =>
                {
                    serviceConfig.UseNLog();

                    serviceInstance.ConstructUsing(
                        () => new SignalRService());

                    serviceInstance.WhenStarted(
                        execute => execute.OnStart(null));

                    serviceInstance.WhenStopped(
                        execute => execute.OnStop());
                });

                TimeSpan delay = new TimeSpan(0, 0, 0, 60);
                serviceConfig.EnableServiceRecovery(recoveryOption =>
                {
                    recoveryOption.RestartService(delay);
                    recoveryOption.RestartService(delay);
                    recoveryOption.RestartComputer(delay,
                       System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                       " computer reboot"); // All subsequent failures
                });

                serviceConfig.SetServiceName
                  (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                serviceConfig.SetDisplayName
                  (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                serviceConfig.SetDescription
                  (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                   " is a simple web  application.");

                serviceConfig.StartAutomatically();
            });
        }

    }
}

