using NAudio.Wave;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using System.Collections.Generic;

namespace Soncoord.Player.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SettingsViewModel : BindableBase
    {
        private readonly IOutputsService _outputsService;

        public SettingsViewModel(IOutputsService outputsService)
        {
            _outputsService = outputsService;
            SaveSettings = new DelegateCommand(OnSaveSettingsExecute);
        }

        public DelegateCommand SaveSettings { get; set; }

        public float MinimumGain => -12;
        
        public float MaximumGain => 12;

        public IEnumerable<DirectSoundDeviceInfo> OutputDevices
        {
            get => _outputsService.Devices;
        }

        public IPlayerOutputSettings OutputSettings
        {
            get => _outputsService.GetSettings();
        }

        private void OnSaveSettingsExecute()
        {
            _outputsService.Save();
        }
    }
}
