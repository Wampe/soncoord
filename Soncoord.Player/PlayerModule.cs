﻿using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
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
            regionManager.RegisterViewWithRegion(Regions.PlayerPlaylist, typeof(Settings));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<Player, PlayerViewModel>();
            ViewModelLocationProvider.Register<Controller, ControllerViewModel>();
            ViewModelLocationProvider.Register<Playlist, PlaylistViewModel>();
        }
    }
}