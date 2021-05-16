using NAudio.Extras;
using NAudio.Wave;
using System.Collections.Generic;

namespace Soncoord.Infrastructure.Interfaces.Services
{
    public interface IOutputsService
    {
        IEnumerable<DirectSoundDeviceInfo> Devices { get; }
        DirectSoundDeviceInfo GetClickTrackOutputDevice();
        void SetClickTrackOutputDevice(DirectSoundDeviceInfo device);
        DirectSoundDeviceInfo GetSongTrackOutputDevice();
        void SetSongTrackOutputDevice(DirectSoundDeviceInfo device);
        EqualizerBand[] GetEqualizer();
        void SetEqualizer(EqualizerBand[] bands);
        void Save();
        void Load();
        IPlayerOutputSettings GetSettings();
        void SetSettings(IPlayerOutputSettings settings);
    }
}
