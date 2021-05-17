using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;

namespace Soncoord.Client.WPF
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            CallAudioPlayer = new DelegateCommand(() =>
            {
                _regionManager.RequestNavigate(
                    Regions.ShellContent,
                    "Dashboard"
                );
            });

            CallSongManager = new DelegateCommand(() =>
            {
                _regionManager.RequestNavigate(
                    Regions.ShellContent,
                    "SongManager"
                );
            });
        }

        public DelegateCommand CallAudioPlayer { get; set; }
        public DelegateCommand CallSongManager { get; set; }
    }
}
