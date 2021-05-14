using NAudio.Extras;
using NAudio.Wave;
using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces;

namespace Soncoord.Infrastructure.Models
{
    public class PlayerOutputSettings : BindableBase, IPlayerOutputSettings
    {
        public PlayerOutputSettings()
        {
            EqualizerBands = EqualizerPresets.Presets["Flat"];
        }

        private DirectSoundDeviceInfo _clickTrackOutputDevice;
        public DirectSoundDeviceInfo ClickTrackOutputDevice
        {
            get => _clickTrackOutputDevice;
            set => SetProperty(ref _clickTrackOutputDevice, value);
        }

        private DirectSoundDeviceInfo _songTrackOutputDevice;
        public DirectSoundDeviceInfo SongTrackOutputDevice
        { 
            get => _songTrackOutputDevice;
            set => SetProperty(ref _songTrackOutputDevice, value);
        }

        private EqualizerBand[] _equalizerBands;
        public EqualizerBand[] EqualizerBands 
        {
            get => _equalizerBands;
            set => SetProperty(ref _equalizerBands, value);
        }
    }
}
