using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
using Soncoord.SongManager.ViewModels;
using Soncoord.SongManager.Views;

namespace Soncoord.SongManager
{
    public class SongManagerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();           
            regionManager.RegisterViewWithRegion(Regions.ShellContent, typeof(SongManager));
            regionManager.RegisterViewWithRegion(Regions.SongList, typeof(SongList));
            regionManager.RegisterViewWithRegion(Regions.SongDetail, typeof(SongDetail));
            regionManager.RegisterViewWithRegion(Regions.SongImport, typeof(SongImport));
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<SongManager, SongManagerViewModel>();
            ViewModelLocationProvider.Register<SongDetail, SongDetailViewModel>();
            ViewModelLocationProvider.Register<SongList, SongListViewModel>();
            ViewModelLocationProvider.Register<SongImport, SongImportViewModel>();
        }
    }
}
