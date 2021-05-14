using NAudio.Extras;
using NAudio.Wave;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface IPlayerOutputSettings
    {
        DirectSoundDeviceInfo ClickTrackOutputDevice { get; set; }
        DirectSoundDeviceInfo SongTrackOutputDevice { get; set; }
        EqualizerBand[] EqualizerBands { get; set; }
    }
}
