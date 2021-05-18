using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;

namespace Soncoord.SongManager.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SongDetailViewModel : BindableBase, INavigationAware
    {
        private readonly IPlaylistService _playlistService;
        private readonly ISongsService _songsService;

        public SongDetailViewModel(ISongsService songsService, IPlaylistService playlistService)
        {
            _songsService = songsService;
            _playlistService = playlistService;

            SaveSettings = new DelegateCommand(OnSaveSettingsExecute);
            SelectClickTrack = new DelegateCommand(OnSelectClickTrackExecute);
            SelectMusicTrack = new DelegateCommand(OnSelectMusicTrackExecute);
            AddToPlaylist = new DelegateCommand(OnAddToPlaylistExecute);
        }

        public DelegateCommand SaveSettings { get; set; }
        public DelegateCommand SelectClickTrack { get; set; }
        public DelegateCommand SelectMusicTrack { get; set; }
        public DelegateCommand AddToPlaylist { get; set; }

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

        private void OnSaveSettingsExecute()
        {
            _songsService.SaveSettings(SelectedSong, SongSettings);
        }
    }
}
