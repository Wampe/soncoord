using NAudio.Extras;
using NAudio.Wave;
using Newtonsoft.Json;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System.Collections.Generic;
using System.IO;

namespace Soncoord.Business.Player
{
    public class OutputsService : IOutputsService
    {
        public OutputsService()
        {
            Load();
        }

        private IPlayerOutputSettings OutputSettings { get; set; }
        public IEnumerable<DirectSoundDeviceInfo> Devices => DirectSoundOut.Devices;

        public DirectSoundDeviceInfo GetClickTrackOutputDevice()
        {
            return OutputSettings.ClickTrackOutputDevice;
        }

        public EqualizerBand[] GetEqualizer()
        {
            return OutputSettings.EqualizerBands;
        }

        public DirectSoundDeviceInfo GetSongTrackOutputDevice()
        {
            return OutputSettings.SongTrackOutputDevice;
        }

        public void Load()
        {
            OutputSettings = LoadSettings();
        }

        public void Save()
        {
            SaveSettings();
        }

        public void SetClickTrackOutputDevice(DirectSoundDeviceInfo device)
        {
            OutputSettings.ClickTrackOutputDevice = device;
        }

        public void SetEqualizer(EqualizerBand[] bands)
        {
            OutputSettings.EqualizerBands = bands;
        }

        public void SetSongTrackOutputDevice(DirectSoundDeviceInfo device)
        {
            OutputSettings.SongTrackOutputDevice = device;
        }

        public IPlayerOutputSettings GetSettings()
        {
            return OutputSettings;
        }

        public void SetSettings(IPlayerOutputSettings settings)
        {
            OutputSettings = settings;
        }

        private IPlayerOutputSettings LoadSettings()
        {
            if (Directory.Exists(Globals.PlayerPath))
            {
                if (File.Exists(Globals.PlayerOutputSettingsFile))
                {
                    IPlayerOutputSettings settings;
                    using (var file = File.OpenText(Globals.PlayerOutputSettingsFile))
                    {
                        var serializer = new JsonSerializer();
                        settings = serializer.Deserialize(file, typeof(PlayerOutputSettings)) as IPlayerOutputSettings;
                    }

                    return settings;
                }
            }

            return new PlayerOutputSettings();
        }

        private void SaveSettings()
        {
            if (!Directory.Exists(Globals.PlayerPath))
            {
                Directory.CreateDirectory(Globals.PlayerPath);
            }

            using (var file = File.CreateText(Globals.PlayerOutputSettingsFile))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, OutputSettings);
            }
        }
    }
}
