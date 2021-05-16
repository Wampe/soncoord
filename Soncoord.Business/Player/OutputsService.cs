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
        private IPlayerOutputSettings _outputSettings;
        public IEnumerable<DirectSoundDeviceInfo> Devices => DirectSoundOut.Devices;

        public OutputsService()
        {
            Load();
        }

        public DirectSoundDeviceInfo GetClickTrackOutputDevice()
        {
            return _outputSettings.ClickTrackOutputDevice;
        }

        public EqualizerBand[] GetEqualizer()
        {
            return _outputSettings.EqualizerBands;
        }

        public DirectSoundDeviceInfo GetSongTrackOutputDevice()
        {
            return _outputSettings.SongTrackOutputDevice;
        }

        public void Load()
        {
            _outputSettings = LoadSettings();
        }

        public void Save()
        {
            SaveSettings();
        }

        public void SetClickTrackOutputDevice(DirectSoundDeviceInfo device)
        {
            _outputSettings.ClickTrackOutputDevice = device;
        }

        public void SetEqualizer(EqualizerBand[] bands)
        {
            _outputSettings.EqualizerBands = bands;
        }

        public void SetSongTrackOutputDevice(DirectSoundDeviceInfo device)
        {
            _outputSettings.SongTrackOutputDevice = device;
        }
        
        public IPlayerOutputSettings GetSettings()
        {
            return _outputSettings;
        }

        public void SetSettings(IPlayerOutputSettings settings)
        {
            _outputSettings = settings;
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
                serializer.Serialize(file, _outputSettings);
            }
        }
    }
}
