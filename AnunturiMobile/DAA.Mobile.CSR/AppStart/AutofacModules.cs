using Autofac;
using Anunturi.Mobile.Services.Common.Alerts;
using Anunturi.Mobile.Services.Common.Navigation;
using Anunturi.Mobile.Services.Logging;
using Anunturi.Mobile.ViewModels;
using System.Reflection;
using Module = Autofac.Module;

namespace Anunturi.Mobile.AppStart
{
    public class AutofacModules: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterViewModels(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(IAlertService).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("Service") && t.Name != nameof(NavigationService) && t.Name != nameof(DebugLoggingService))
                .AsImplementedInterfaces();

            builder.RegisterType<DebugLoggingService>().As<ILoggingService>();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseViewModel).GetTypeInfo().Assembly)
                .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(BaseViewModel)));
        }
    }
}
