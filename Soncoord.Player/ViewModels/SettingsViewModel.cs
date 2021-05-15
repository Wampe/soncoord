using NAudio.Wave;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Models;
using System.Collections.Generic;
using System.IO;

namespace Soncoord.Player.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel()
        {
            SaveSettings = new DelegateCommand(OnSaveSettingsExecute);
            SelectedOutputSettings = LoadSettings();
        }

        public DelegateCommand SaveSettings { get; set; }

        public float MinimumGain => -12;
        
        public float MaximumGain => 12;

        public IEnumerable<DirectSoundDeviceInfo> OutputDevices
        { 
            get => DirectSoundOut.Devices;
        }
        
        private IPlayerOutputSettings _selectedOutputSettings;
        public IPlayerOutputSettings SelectedOutputSettings
        {
            get => _selectedOutputSettings;
            set => SetProperty(ref _selectedOutputSettings, value);
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

        private void OnSaveSettingsExecute()
        {
            if (!Directory.Exists(Globals.PlayerPath))
            {
                Directory.CreateDirectory(Globals.PlayerPath);
            }

            using (var file = File.CreateText(Globals.PlayerOutputSettingsFile))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, SelectedOutputSettings);
            }
        }
    }
}
