using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System.Collections.ObjectModel;

namespace Soncoord.SongManager.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SongDetailViewModel : BindableBase, INavigationAware
    {
        private readonly IPlaylistService _playlistService;
        private readonly ISongsService _songsService;
        private readonly IStreamerSonglistService _providerService;

        public SongDetailViewModel(
            ISongsService songsService,
            IPlaylistService playlistService,
            IStreamerSonglistService songListService)
        {
            _songsService = songsService;
            _playlistService = playlistService;
            _providerService = songListService;

            SaveSettings = new DelegateCommand(OnSaveSettingsExecute);
            SelectClickTrack = new DelegateCommand(OnSelectClickTrackExecute);
            SelectMusicTrack = new DelegateCommand(OnSelectMusicTrackExecute);
            AddToPlaylist = new DelegateCommand(OnAddToPlaylistExecute);
            AddToQueue = new DelegateCommand(OnAddToQueueExecute);
        }

        public DelegateCommand SaveSettings { get; set; }
        public DelegateCommand SelectClickTrack { get; set; }
        public DelegateCommand SelectMusicTrack { get; set; }
        public DelegateCommand AddToPlaylist { get; set; }
        public DelegateCommand AddToQueue { get; set; }

        private ISongSetting _songSettings;
        public ISongSetting SongSettings
        {
            get => _songSettings;
            set => SetProperty(ref _songSettings, value);
        }

        private ISong _selectedSong;
        public ISong SelectedSong
        {
            get => _selectedSong;
            set => SetProperty(ref _selectedSong, value);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var song = navigationContext.Parameters.GetValue<ISong>("Song");
            SelectedSong = song;
            SongSettings = _songsService.GetSettings(song);
        }

        private void OnSelectMusicTrackExecute()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SongSettings.MusicTrackPath = openFileDialog.FileName;
            }
        }

        private void OnSelectClickTrackExecute()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SongSettings.ClickTrackPath = openFileDialog.FileName;
            }
        }

        private void OnAddToPlaylistExecute()
        {
            _playlistService.Add(SelectedSong);
        }

        private void OnAddToQueueExecute()
        {
            var songRequest = new AddSongRequest
            {
                SongId = SelectedSong.Id,
                AllowFirstPosition = true,
                AllowUpdate = true,
                Note = "Request by Streamer",
                Requests = new Collection<IAddSongRequestUser>
                {
                    new AddSongRequestUser
                    {
                        Amount = 0,
                        Name = "Wampe"
                    }
                }
            };

            _providerService.AddSongToQueueAsync(songRequest);
        }

        private void OnSaveSettingsExecute()
        {
            _songsService.SaveSettings(SelectedSong, SongSettings);
        }
    }
}
