using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Threading;

namespace Soncoord.SongRequests.ViewModels
{
    public class QueueViewModel : BindableBase
    {
        // Still needed or is it enough to create a new call after an event?
        private readonly DispatcherTimer _queueTimer;

        private readonly HttpClient _httpClient;
        private readonly IPlaylistService _playlistService;
        private readonly ISongsService _songsService;

        public QueueViewModel(ISongsService songsService, IPlaylistService playlistService)
        {
            _songsService = songsService;
            _playlistService = playlistService;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.streamersonglist.com")
            };

            _queueTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromSeconds(15)
            };

            _queueTimer.Tick += QueueTimerTicked;
            _queueTimer.Start();

            AddToPlaylist = new DelegateCommand<Queue>(OnAddToPlaylistExecute, OnAddToPlaylistCanExecute);
            SongRequestQueue = new ObservableCollection<Queue>();

            LoadSongs();
        }

        public DelegateCommand<Queue> AddToPlaylist { get; set; }
        public ObservableCollection<Queue> SongRequestQueue { get; set; }

        private void OnAddToPlaylistExecute(Queue queue)
        {
            var song = _songsService.GetSongById(queue.SongId);
            if (song == null)
            {
                return;
            }

            _playlistService.Add(song);
        }

        private bool OnAddToPlaylistCanExecute(Queue queue)
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
            LoadSongs();
        }

        private async void LoadSongs()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/wampe/queue");
            //var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/2557/queue");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var songQuery = JsonConvert.DeserializeObject<QueueQuery>(result);

            SongRequestQueue.Clear();
            foreach (var item in songQuery.List)
            {
                SongRequestQueue.Add(item);
            }
        }
    }
}
