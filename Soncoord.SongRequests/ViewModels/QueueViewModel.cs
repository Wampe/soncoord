using Prism.Commands;
using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Soncoord.SongRequests.ViewModels
{
    public class QueueViewModel : BindableBase
    {
        // Still needed or is it enough to create a new call after an event?
        private readonly DispatcherTimer _queueTimer;

        private readonly IPlaylistService _playlistService;
        private readonly ISongsService _songsService;
        private readonly IStreamerSonglistService _providerService;

        public QueueViewModel(
            ISongsService songsService,
            IPlaylistService playlistService,
            IStreamerSonglistService providerService)
        {
            _providerService = providerService;
            _songsService = songsService;
            _playlistService = playlistService;
            _queueTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromSeconds(15)
            };

            _queueTimer.Tick += QueueTimerTicked;
            _queueTimer.Start();

            AddToPlaylist = new DelegateCommand<QueueSongRequest>(OnAddToPlaylistExecute, OnAddToPlaylistCanExecute);
            SongRequestQueue = new ObservableCollection<QueueSongRequest>();

            LoadSongRequests();
        }

        public DelegateCommand<QueueSongRequest> AddToPlaylist { get; set; }
        public ObservableCollection<QueueSongRequest> SongRequestQueue { get; set; }

        private void OnAddToPlaylistExecute(QueueSongRequest queue)
        {
            var song = _songsService.GetSongById(queue.SongId);
            if (song == null)
            {
                return;
            }

            _playlistService.Add(song);
        }

        private bool OnAddToPlaylistCanExecute(QueueSongRequest queue)
        {
            var song = _songsService.GetSongById(queue.SongId);
            if (song == null)
            {
                return false;
            }

            var settings = _songsService.GetSettings(song);
            if (settings == null)
            {
                return false;
            }

            return !string.IsNullOrEmpty(settings.MusicTrackPath);
    
        }

        private void QueueTimerTicked(object sender, EventArgs e)
        {
            LoadSongRequests();
        }

        private async void LoadSongRequests()
        {
            var requests = await _providerService.GetSongRequests();
            SongRequestQueue.Clear();
            foreach (var item in requests)
            {
                SongRequestQueue.Add(item);
            }
        }
    }
}
