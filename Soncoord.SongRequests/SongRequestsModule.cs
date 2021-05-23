using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Business.Provider;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.SongRequests.ViewModels;
using Soncoord.SongRequests.Views;

namespace Soncoord.SongRequests
{
    public class SongRequestsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.DashboardQueue, typeof(Queue));
            regionManager.RegisterViewWithRegion(Regions.ShellContent, typeof(RequestsBrowser));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IStreamerSonglistService, StreamerSonglistService>();
            ViewModelLocationProvider.Register<Queue, QueueViewModel>();
        }
    }
}
