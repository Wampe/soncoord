using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Soncoord.Client.WPF.ViewModels;
using Soncoord.Client.WPF.Views;
using Soncoord.Infrastructure;
using Soncoord.Player;
using Soncoord.SongManager;
using Soncoord.SongRequests;
using System.Windows;

namespace Soncoord.Client.WPF
{
    class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell(DependencyObject shell)
        {
            base.InitializeShell(shell);

            var regionManager = RegionManager.GetRegionManager(shell);
            regionManager.RegisterViewWithRegion(Regions.ShellContent, typeof(Dashboard));
            regionManager.RegisterViewWithRegion(Regions.NotificationContent, typeof(Notification));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<PlayerModule>();
            moduleCatalog.AddModule<SongManagerModule>();
            moduleCatalog.AddModule<SongRequestsModule>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<Dashboard, DashboardViewModel>();
            ViewModelLocationProvider.Register<Notification, NotificationViewModel>();
            ViewModelLocationProvider.Register<Shell, ShellViewModel>();
        }
    }
}
