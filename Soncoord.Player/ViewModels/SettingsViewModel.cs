using NAudio.Wave;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Soncoord.Player.ViewModels
{
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
        
        private PlayerOutputSettings _selectedOutputSettings;
        public PlayerOutputSettings SelectedOutputSettings
        {
            get => _selectedOutputSettings;
            set => SetProperty(ref _selectedOutputSettings, value);
        }

        private PlayerOutputSettings LoadSettings()
        {
            if (Directory.Exists(Globals.PlayerPath))
            {
                if (File.Exists(Globals.PlayerOutputSettingsFile))
                {
                    PlayerOutputSettings settings;
                    using (var file = File.OpenText(Globals.PlayerOutputSettingsFile))
                    {
                        var serializer = new JsonSerializer();
                        settings = serializer.Deserialize(file, typeof(PlayerOutputSettings)) as PlayerOutputSettings;
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
