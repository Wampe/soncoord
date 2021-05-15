using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Soncoord.Infrastructure.Events;
using Soncoord.Infrastructure.Interfaces;
using System;
using System.Windows.Threading;

namespace Soncoord.Player.ViewModels
{
    public class ControllerViewModel : BindableBase
    {
        private readonly DispatcherTimer _positionTimer;

        public ControllerViewModel(IEventAggregator eventAggregator)
        {
            _positionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _positionTimer.Tick += PositionTimerTick;

            PlayCommand = new DelegateCommand(OnPlayCommandExecute);
            StopCommand = new DelegateCommand(OnStopCommandExecute);

            UpdateAudioPosition(new TimeSpan(0));

            eventAggregator.GetEvent<LoadSongIntoControllerEvent>().Subscribe((LoadSongIntoControllerParameters parameter) =>
            {
                SelectedSong = parameter.Song;
                SongSettings = parameter.Settings;
            });
        }

        public DelegateCommand PlayCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }
        internal ISongSetting SongSettings { get; set; }

        private ISong _selectedSong;
        public ISong SelectedSong
        {
            get => _selectedSong;
            set => SetProperty(ref _selectedSong, value);
        }

        private TimeSpan _audioPosition;
        public TimeSpan AudioPosition
        {
            get => _audioPosition;
            set => SetProperty(ref _audioPosition, value);
        }

        private bool _isReversedAudioPosition;
        public bool IsReversedAudioPosition
        {
            get => _isReversedAudioPosition;
            set => SetProperty(ref _isReversedAudioPosition, value);
        }

        private void OnStopCommandExecute()
        {
            _positionTimer.Stop();
            UpdateAudioPosition(new TimeSpan(0));
        }

        private void OnPlayCommandExecute()
        {
        }

        private void PositionTimerTick(object sender, EventArgs e)
        {
            //UpdateAudioPosition(_songReader.CurrentTime);
        }

        private void UpdateAudioPosition(TimeSpan value)
        {
            AudioPosition = value;
        }
    }
}
