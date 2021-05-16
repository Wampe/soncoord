using Prism.Commands;
using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using System;

namespace Soncoord.Player.ViewModels
{
    public class ControllerViewModel : BindableBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IPlayerExecuter _executer;

        public ControllerViewModel(IPlaylistService playlistService, IPlayerExecuter executer)
        {
            _playlistService = playlistService;
            _executer = executer;

            Play = new DelegateCommand(OnPlayCommandExecute, OnPlayCommandCanExecute);
            Stop = new DelegateCommand(OnStopCommandExecute);

            _executer.PositionChanged += PositionUpdated;
            _executer.Started += PlayerStarted;
        }

        public DelegateCommand Play { get; set; }
        public DelegateCommand Stop { get; set; }

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

        private void OnStopCommandExecute()
        {
            _executer.Stop();
        }

        private bool OnPlayCommandCanExecute()
        {
            return true;

            // ToDo: Update Handling
            //return !_playlistService.IsPlaylistEmpty();
        }

        private void OnPlayCommandExecute()
        {
            SelectedSong = _playlistService.GetNextSong();
            _executer.Play(_playlistService.GetSongSettings(SelectedSong));
        }

        private void PositionUpdated(object sender, TimeSpan e)
        {
            Position = e;
        }

        private void PlayerStarted(object sender, TimeSpan e)
        {
            TotalTime = e;
        }
    }
}
