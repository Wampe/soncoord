using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace Soncoord.Audio.Visualizer
{
    public class AudioVisualizerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("VisualizerRegion", typeof(AudioVisualizer));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<AudioVisualizer, AudioVisualizerViewModel>();
        }
    }
}