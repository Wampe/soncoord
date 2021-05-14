using NAudio.Extras;
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

        private AudioFileReader _clickReader;
        private AudioFileReader _songReader;

        private Equalizer _equalizer;
        private readonly EqualizerBand[] _bands;

        private readonly DispatcherTimer _positionTimer;

        public PlayerViewModel()
        {
            _positionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _positionTimer.Tick += PositionTimerTick;

            // EQ Settings simliar to Techno preset of Winamp
            //_bands = new EqualizerBand[]
            //{
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 70, Gain = 4.5f},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 180, Gain = 3.375f},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 320, Gain = 0},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 600, Gain = -3.75f},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 1000, Gain = -3.375f},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 3000, Gain = 0},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 6000, Gain = 4.5f},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 12000, Gain = 5.625f},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 14000, Gain = 5.658f},
            //    new EqualizerBand {Bandwidth = 0.8f, Frequency = 16000, Gain = 5.25f},
            //};

            //Devices = new ObservableCollection<DirectSoundDeviceInfo>();
            StopCommand = new DelegateCommand(StopCommandExecute, CanStopCommandExecute)
                .ObservesProperty(() => IsPlaying);
            PlayCommand = new DelegateCommand(PlayCommandExecute, CanPlayCommandExecute)
                .ObservesProperty(() => IsPlaying);
                //.ObservesProperty(() => SelectedClickDevice)
                //.ObservesProperty(() => SelectedSongDevice);

            UpdateAudioPosition(new TimeSpan(0));
            //LoadDevices();
        }

        public DelegateCommand PlayCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }
        //public ObservableCollection<DirectSoundDeviceInfo> Devices { get; set; }

        //public float MinimumGain => -12;
        //public float MaximumGain => 12;

        private TimeSpan _audioPosition;
        public TimeSpan AudioPosition
        {
            get => _audioPosition;
            set => SetProperty(ref _audioPosition, value);
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        //private DirectSoundDeviceInfo _selectedClickDevice;
        //public DirectSoundDeviceInfo SelectedClickDevice
        //{
        //    get => _selectedClickDevice;
        //    set => SetProperty(ref _selectedClickDevice, value);
        //}

        //private DirectSoundDeviceInfo _selectedSongDevice;
        //public DirectSoundDeviceInfo SelectedSongDevice
        //{
        //    get => _selectedSongDevice;
        //    set => SetProperty(ref _selectedSongDevice, value);
        //}

        private bool CanPlayCommandExecute()
        {
            return true;
                //!IsPlaying
                //&& SelectedSongDevice != null
                //&& SelectedClickDevice != null;
        }

        private void PlayCommandExecute()
        {
            //_clickReader = new AudioFileReader(@"");
            //_songReader = new AudioFileReader(@"");
            
            //_outputClick = new DirectSoundOut(SelectedClickDevice.Guid);
            //_outputClick.PlaybackStopped += OnPlaybackStopped;
            //_outputClick.Init(new OffsetSampleProvider(_clickReader.ToSampleProvider())
            //{
            //    DelayBy = TimeSpan.FromSeconds(1)
            //});

            //var sampleProvider = new OffsetSampleProvider(_songReader.ToSampleProvider())
            //{
            //    DelayBy = _clickReader.TotalTime - _songReader.TotalTime + TimeSpan.FromSeconds(1)
            //};
            //_equalizer = new Equalizer(sampleProvider, _bands);
            //_outputSong = new DirectSoundOut(SelectedSongDevice.Guid);
            //_outputSong.PlaybackStopped += OnPlaybackStopped;
            //_outputSong.Init(_equalizer);

            //_outputClick.Play();
            //_outputSong.Play();
            //_positionTimer.Start();

            //IsPlaying = true;
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

        //private void LoadDevices()
        //{
        //    foreach (var device in DirectSoundOut.Devices)
        //    {
        //        Devices.Add(device);
        //    }
        //}

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
