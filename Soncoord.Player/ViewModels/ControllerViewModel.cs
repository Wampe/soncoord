using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Soncoord.Infrastructure.Events;
using Soncoord.Infrastructure.Interfaces;
using System;

namespace Soncoord.Player.ViewModels
{
    public class ControllerViewModel : BindableBase
    {
        public ControllerViewModel(IEventAggregator eventAggregator)
        {
            PlayerExecuter = new PlayerExecuter();
            PlayCommand = new DelegateCommand(OnPlayCommandExecute, OnPlayCommandCanExecute)
                .ObservesProperty(() => SongSettings);
            StopCommand = new DelegateCommand(OnStopCommandExecute);

            eventAggregator.GetEvent<LoadSongIntoControllerEvent>().Subscribe((LoadSongIntoControllerParameters parameter) =>
            {
                SelectedSong = parameter.Song;
                SongSettings = parameter.Settings;
                RaisePropertyChanged("SongSettings");
            });

            PlayerExecuter.PositionUpdated += PositionUpdated;
            PlayerExecuter.Started += PlayerStarted;
        }

        public DelegateCommand PlayCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }
        internal ISongSetting SongSettings { get; set; }
        internal PlayerExecuter PlayerExecuter { get; set; }

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

        private TimeSpan _audioTotalTime;
        public TimeSpan AudioTotalTime
        {
            get => _audioTotalTime;
            set => SetProperty(ref _audioTotalTime, value);
        }

        private void OnStopCommandExecute()
        {
            PlayerExecuter.Stop();
        }

        private bool OnPlayCommandCanExecute()
        {
            return SongSettings != null
                && !string.IsNullOrEmpty(SongSettings.ClickTrackPath) 
                && !string.IsNullOrEmpty(SongSettings.MusicTrackPath);
        }

        private void OnPlayCommandExecute()
        {
            PlayerExecuter.Play(SongSettings.ClickTrackPath, SongSettings.MusicTrackPath);
        }

        private void PositionUpdated(object sender, TimeSpan e)
        {
            AudioPosition = e;
        }

        private void PlayerStarted(object sender, TimeSpan e)
        {
            AudioTotalTime = e;
        }
    }
}
