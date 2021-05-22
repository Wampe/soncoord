using Prism.Commands;
using Prism.Mvvm;
using Soncoord.Business.Player;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using System;

namespace Soncoord.Player.ViewModels
{
    public class ControllerViewModel : BindableBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IOutputsService _outputsService;

        public ControllerViewModel(IPlaylistService playlistService, IOutputsService outputsService)
        {
            _outputsService = outputsService;
            _playlistService = playlistService;

            Stop = new DelegateCommand(OnStopCommandExecute);
            Play = new DelegateCommand(OnPlayCommandExecute, OnCommandCanExecute)
                .ObservesProperty(() => IsPlaying);
            Next = new DelegateCommand(OnNextCommandExecute, OnCommandCanExecute)
                .ObservesProperty(() => IsPlaying);
        }

        public DelegateCommand Play { get; set; }
        public DelegateCommand Stop { get; set; }
        public DelegateCommand Next { get; set; }
        private SongExecuter SongExecuter { get; set; }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        private ISong _selectedSong;
        public ISong SelectedSong
        {
            get => _selectedSong;
            set => SetProperty(ref _selectedSong, value);
        }

        private TimeSpan _position;
        public TimeSpan Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private TimeSpan _totalTime;
        public TimeSpan TotalTime
        {
            get => _totalTime;
            set => SetProperty(ref _totalTime, value);
        }

        private bool OnCommandCanExecute()
        {
            return !IsPlaying;
        }

        private void OnStopCommandExecute()
        {
            SongExecuter?.Stop();
        }

        private void OnPlayCommandExecute()
        {
            PlaySong();
        }

        private void OnNextCommandExecute()
        {
            PlayNextSong();
        }

        private void PositionUpdated(object sender, TimeSpan e)
        {
            Position = e;
        }

        private void PlayerStarted(object sender, TimeSpan e)
        {
            TotalTime = e;
        }

        private void PlayerEnded(object sender, EventArgs e)
        {
            UnregisterExecuter();
            PlayNextSong();
        }

        private void PlayerStopped(object sender, EventArgs e)
        {
            UnregisterExecuter();
        }

        private void PlayNextSong()
        {
            if (SelectedSong != null)
            {
                _playlistService.Remove(SelectedSong);
            }

            PlaySong();
        }

        private void PlaySong()
        {
            SelectedSong = _playlistService.GetNextSong();
            if (SelectedSong == null)
            {
                return;
            }

            var songSettings = _playlistService.GetSongSettings(SelectedSong);
            var outputSettings = _outputsService.GetSettings();

            SongExecuter = new SongExecuter(songSettings, outputSettings);
            SongExecuter.PositionChanged += PositionUpdated;
            SongExecuter.Started += PlayerStarted;
            SongExecuter.Stopped += PlayerStopped;
            SongExecuter.Ended += PlayerEnded;

            SongExecuter.Play();
            IsPlaying = true;
        }

        private void UnregisterExecuter()
        {
            SongExecuter.PositionChanged -= PositionUpdated;
            SongExecuter.Started -= PlayerStarted;
            SongExecuter.Stopped -= PlayerStopped;
            SongExecuter.Ended -= PlayerEnded;
            SongExecuter.Dispose();
            SongExecuter = null;

            IsPlaying = false;
        }
    }
}
