using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Business.Player;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Player.ViewModels;
using Soncoord.Player.Views;

namespace Soncoord.Player
{
    public class PlayerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.ShellContent, typeof(Player));
            regionManager.RegisterViewWithRegion(Regions.PlayerController, typeof(Controller));
            regionManager.RegisterViewWithRegion(Regions.PlayerContext, typeof(Playlist));
            regionManager.RegisterViewWithRegion(Regions.PlayerContext, typeof(Settings));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPlaylistService, PlaylistService>();
            containerRegistry.RegisterSingleton<IOutputsService, OutputsService>();
            containerRegistry.RegisterSingleton<IPlayerExecuter, PlayerExecuter>();

            ViewModelLocationProvider.Register<Controller, ControllerViewModel>();
            ViewModelLocationProvider.Register<Playlist, PlaylistViewModel>();
            ViewModelLocationProvider.Register<Settings, SettingsViewModel>();
        }
    }
}