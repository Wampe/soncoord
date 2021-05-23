using Prism.Commands;
using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace Soncoord.SongRequests.ViewModels
{
    public class QueueViewModel : BindableBase
    {
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
            _providerService.UserChanged += UserChanged;

            _songsService = songsService;
            _playlistService = playlistService;
            
            _queueTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
            {
                Interval = TimeSpan.FromSeconds(30)
            };
            _queueTimer.Tick += QueueTimerTicked;

            AddToPlaylist = new DelegateCommand<QueueSongRequest>(OnAddToPlaylistExecute, OnAddToPlaylistCanExecute);
            Played = new DelegateCommand<QueueSongRequest>(OnPlayedExecute);
            Remove = new DelegateCommand<QueueSongRequest>(OnRemoveExecute);
            SongRequestQueue = new ObservableCollection<QueueSongRequest>();
            
            IsAuthorized = _providerService.IsAuthorized;
        }

        public DelegateCommand<QueueSongRequest> AddToPlaylist { get; set; }
        public DelegateCommand<QueueSongRequest> Played { get; set; }
        public DelegateCommand<QueueSongRequest> Remove { get; set; }
        public ObservableCollection<QueueSongRequest> SongRequestQueue { get; set; }
        private IStreamerQueueSettings QueueSettings { get; set; }

        private bool _isAuthorized;
        public bool IsAuthorized
        {
            get => _isAuthorized;
            set => SetProperty(ref _isAuthorized, value);
        }

        private bool _isQueueActive;
        public bool IsQueueActive
        {
            get => _isQueueActive;
            set => SetProperty(ref _isQueueActive, value);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == "IsQueueActive")
            {
                QueueSettings.RequestsActive = IsQueueActive;
                _providerService.SetQueueSettingsAsync(QueueSettings);

                if (IsQueueActive)
                {
                    ActivateQueue();
                }
                else
                {
                    _queueTimer.Stop();
                }
            }
        }

        private void ActivateQueue()
        {
            _queueTimer.Start();
            LoadSongRequests();
        }

        private async void UserChanged(object sender, EventArgs e)
        {
            IsAuthorized = _providerService.IsAuthorized;

            if (IsAuthorized)
            {
                QueueSettings = await _providerService.GetQueueSettingsAsync();
                _isQueueActive = QueueSettings.RequestsActive;
                RaisePropertyChanged("IsQueueActive");
            }
        }

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

        private async void OnPlayedExecute(QueueSongRequest request)
        {
            await _providerService.SetSongAsPlayedAsync(request);
            SongRequestQueue.Remove(request);
        }

        private async void OnRemoveExecute(QueueSongRequest request)
        {
            await _providerService.RemoveSongFromQueue(request);
            SongRequestQueue.Remove(request);
        }

        private void QueueTimerTicked(object sender, EventArgs e)
        {
            LoadSongRequests();
        }

        private async void LoadSongRequests()
        {
            var requests = await _providerService.GetSongRequestsAsync();
            SongRequestQueue.Clear();
            foreach (var item in requests)
            {
                SongRequestQueue.Add(item);
            }
        }
    }
}
