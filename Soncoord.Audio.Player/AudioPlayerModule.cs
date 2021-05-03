using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace Soncoord.Audio.Player
{
    public class AudioPlayerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(AudioPlayer));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<AudioPlayer, AudioPlayerViewModel>();
        }
    }
}