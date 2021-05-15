using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Events;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Soncoord.SongManager.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SongImportViewModel : BindableBase
    {
        private readonly HttpClient _httpClient;
        private readonly IEventAggregator _eventAggregator;

        public SongImportViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.streamersonglist.com")
            };

            SynchronizeSongs = new DelegateCommand(OnSynchronizeSongsExecute, OnSynchronizeSongsCanExecute)
                .ObservesProperty(() => IsImportActive);
        }

        public DelegateCommand SynchronizeSongs { get; set; }
        internal ICollection<ISong> Songs { get; set; }

        private int _importCount;
        public int ImportCount
        {
            get => _importCount;
            set => SetProperty(ref _importCount, value);
        }

        private bool _isImportActive;
        public bool IsImportActive
        {
            get => _isImportActive;
            set => SetProperty(ref _isImportActive, value);
        }

        private bool OnSynchronizeSongsCanExecute()
        {
            return !IsImportActive;
        }

        private async void OnSynchronizeSongsExecute()
        {
            IsImportActive = true;
            var songs = await LoadSongs(100, 0, new Collection<ISong>());
            SaveSongs(songs);
            IsImportActive = false;

            ImportCount = songs.Count;

            _eventAggregator.GetEvent<SongsImportedEvent>().Publish();
        }

        private async Task<ICollection<ISong>> LoadSongs(int size, int current, ICollection<ISong> collection)
        {
            var result = await GetSongs(BuildUriQuery(size, current));
            var songQuery = JsonConvert.DeserializeObject<SongQuery>(result);
            
            foreach (var item in songQuery.Items)
            {
                collection.Add(item);
            }

            if (collection.Count < songQuery.Total)
            {
                var songs = await LoadSongs(size, current + 1, collection);
                collection.Concat(songs);
            }

            return collection;
        }

        private async Task<string> GetSongs(Uri url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private Uri BuildUriQuery(int size, int current)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}v1/streamers/wampe/songs");
            //var builder = new UriBuilder($"{_httpClient.BaseAddress}v1/streamers/2557/songs");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["size"] = size.ToString();
            query["current"] = current.ToString();
            query["current"] = current.ToString();
            builder.Query = query.ToString();

            return builder.Uri;
        }

        private void SaveSongs(ICollection<ISong> songs)
        {
            if (!Directory.Exists(Globals.SongsPath))
            {
                Directory.CreateDirectory(Globals.SongsPath);
            }

            foreach (var song in songs)
            {
                // Check if file for song already exists
                // if not then create

                // Beside of a new created song possibly create an seperate definitions file as json
                using (var file = File.CreateText($"{Globals.SongsPath}\\{song.Id}.json"))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, song);
                }
            }
        }
    }
}
