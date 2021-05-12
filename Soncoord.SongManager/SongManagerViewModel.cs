using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using Soncoord.Infrastructure.Models;
using Soncoord.Infrastructure.Interfaces;
using System.Windows.Data;
using System.ComponentModel;

namespace Soncoord.SongManager
{
    // ToDO:
    // Song Manager will just manage the song itself. 
    // At the moment this implementation is for loading the songs from StreamerSonglist
    // This will have to get moved to the planned Streamer SongList Connector

    public class SongManagerViewModel : BindableBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _songsPath;

        public SongManagerViewModel()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.streamersonglist.com")
            };
             _songsPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Soncoord\\Songs";

            Songs = new ObservableCollection<ISong>();
            SongsViewSource = new CollectionViewSource
            {
                Source = Songs,
                
            };
            SongsViewSource.SortDescriptions.Add(new SortDescription("Artist", ListSortDirection.Ascending));
            SongsViewSource.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            SongsViewSource.Filter += SongsViewSourceFilter;

            LoadSongsExternal = new DelegateCommand(OnLoadSongsExternalExecute);
            SaveSongs = new DelegateCommand(OnSaveSongExecute);
            LoadsSongsFromFile = new DelegateCommand(OnLoadSongsFromFileExecute);
        }

        public DelegateCommand LoadSongsExternal { get; set; }
        public DelegateCommand SaveSongs { get; set; }
        public DelegateCommand LoadsSongsFromFile { get; set; }
        internal ObservableCollection<ISong> Songs { get; set; }
        internal CollectionViewSource SongsViewSource { get; set; }

        public ICollectionView SongsView
        {
            get { return SongsViewSource.View; }
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                SetProperty(ref _filterText, value);
                SongsView.Refresh();
            }
        }

        private void SongsViewSourceFilter(object sender, FilterEventArgs e)
        {
            var song = e.Item as ISong;
            e.Accepted = string.IsNullOrWhiteSpace(_filterText)
                || song.Artist.ToLower().Contains(_filterText.ToLower())
                || song.Title.ToLower().Contains(_filterText.ToLower());
        }

        private void OnSaveSongExecute()
        {
            if (!Directory.Exists(_songsPath))
            {
                Directory.CreateDirectory(_songsPath);
            }

            foreach (var song in Songs)
            {
                // Check if file for song already exists
                // if not then create
                
                // Beside of a new created song possibly create an seperate definitions file as json
                using (var file = File.CreateText($"{_songsPath}\\{song.Id}.json"))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, song);
                }
            }
        }

        private void OnLoadSongsFromFileExecute()
        {
            var files = Directory.GetFiles(_songsPath);

            Songs.Clear();

            foreach (var item in files)
            {
                using (var file = File.OpenText(item))
                {
                    var serializer = new JsonSerializer();
                    var song = serializer.Deserialize(file, typeof(Song)) as ISong;
                    Songs.Add(song);
                }
            }
        }

        private void OnLoadSongsExternalExecute()
        {
            Songs.Clear();
            LoadSongs(100, 0);
        }

        private async void LoadSongs(int size, int current)
        {
            var result = await GetSongs(BuildUriQuery(size, current));
            var songQuery = JsonConvert.DeserializeObject<SongQuery>(result);

            foreach (var item in songQuery.Items)
            {
                Songs.Add(item);
            }

            if (Songs.Count < songQuery.Total)
            {
                LoadSongs(100, current + 1);
            }
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
            builder.Query = query.ToString();

            return builder.Uri;
        }
    }
}
