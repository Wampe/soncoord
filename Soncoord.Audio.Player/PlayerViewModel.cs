using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Soncoord.Player
{
    public class PlayerViewModel : BindableBase
    {
        private DirectSoundOut _outputClick;
        private DirectSoundOut _outputSong;

        private Mp3FileReader _clickReader;
        private Mp3FileReader _songReader;

        private readonly DispatcherTimer _positionTimer;

        public PlayerViewModel()
        {
            _positionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _positionTimer.Tick += PositionTimerTick;

            ModuleTitle = "Audio Player 0.1";
            Devices = new ObservableCollection<DirectSoundDeviceInfo>();
            StopCommand = new DelegateCommand(StopCommandExecute, CanStopCommandExecute)
                .ObservesProperty(() => IsPlaying);
            PlayCommand = new DelegateCommand(PlayCommandExecute, CanPlayCommandExecute)
                .ObservesProperty(() => IsPlaying)
                .ObservesProperty(() => SelectedClickDevice)
                .ObservesProperty(() => SelectedSongDevice);

            UpdateAudioPosition(new TimeSpan(0));
            LoadDevices();
        }

        public DelegateCommand PlayCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }
        public ObservableCollection<DirectSoundDeviceInfo> Devices { get; set; }

        private TimeSpan _audioPosition;
        public TimeSpan AudioPosition
        {
            get => _audioPosition;
            set => SetProperty(ref _audioPosition, value);
        }

        private string _moduleTitle;
        public string ModuleTitle
        {
            get => _moduleTitle;
            set => SetProperty(ref _moduleTitle, value);
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        private DirectSoundDeviceInfo _selectedClickDevice;
        public DirectSoundDeviceInfo SelectedClickDevice
        {
            get => _selectedClickDevice;
            set => SetProperty(ref _selectedClickDevice, value);
        }

        private DirectSoundDeviceInfo _selectedSongDevice;
        public DirectSoundDeviceInfo SelectedSongDevice
        {
            get => _selectedSongDevice;
            set => SetProperty(ref _selectedSongDevice, value);
        }

        private bool CanPlayCommandExecute()
        {
            return !IsPlaying
                && SelectedSongDevice != null
                && SelectedClickDevice != null;
        }

        private void PlayCommandExecute()
        {
            _clickReader = new Mp3FileReader(@"");
            _songReader = new Mp3FileReader(@"");
            
            _outputClick = new DirectSoundOut(SelectedClickDevice.Guid);
            _outputClick.PlaybackStopped += OnPlaybackStopped;
            _outputClick.Init(new OffsetSampleProvider(_clickReader.ToSampleProvider())
            {
                DelayBy = TimeSpan.FromSeconds(1)
            });

            _outputSong = new DirectSoundOut(SelectedSongDevice.Guid);
            _outputSong.PlaybackStopped += OnPlaybackStopped;
            _outputSong.Init(new OffsetSampleProvider(_songReader.ToSampleProvider())
            {
                DelayBy = _clickReader.TotalTime - _songReader.TotalTime + TimeSpan.FromSeconds(1)
            });

            _outputClick.Play();
            _outputSong.Play();
            _positionTimer.Start();

            IsPlaying = true;
        }
        
        private bool CanStopCommandExecute()
        {
            return IsPlaying;
        }

        private void StopCommandExecute()
        {
            _outputClick?.Stop();
            _outputSong?.Stop();

            _positionTimer.Stop();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _outputClick.PlaybackStopped -= OnPlaybackStopped;
            _outputClick.Dispose();
            _outputClick = null;

            _outputSong.PlaybackStopped -= OnPlaybackStopped;
            _outputSong.Dispose();
            _outputSong = null;

            _clickReader.Dispose();
            _clickReader = null;

            _songReader.Dispose();
            _songReader = null;

            IsPlaying = false;
            UpdateAudioPosition(new TimeSpan(0));
        }

        private void LoadDevices()
        {
            foreach (var device in DirectSoundOut.Devices)
            {
                Devices.Add(device);
            }
        }

        private void PositionTimerTick(object sender, EventArgs e)
        {
            UpdateAudioPosition(_songReader.CurrentTime);
        }

        private void UpdateAudioPosition(TimeSpan value)
        {
            AudioPosition = value;
        }
    }
}
