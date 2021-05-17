using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<Queue, QueueViewModel>();
        }
    }
}
