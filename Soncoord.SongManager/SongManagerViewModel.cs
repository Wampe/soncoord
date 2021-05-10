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
using System.Collections.Generic;

namespace Soncoord.SongManager
{
    // ToDO:
    // Song Manager will just manage the song itself. 
    // At the moment this implementation is for loading the songs from StreamerSonglist
    // This will have to get moved to the planned Streamer SongList Connector

    // ToDO: Move to seperate Files
    public class SongQuery
    {
        public Song[] Items { get; set; }
        public int Total { get; set; }
    }

    public class Song
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
        public double MinAmount { get; set; }
        public DateTime? LastPlayed { get; set; }
        public int TimesPlayed { get; set; }
        public int NumQueued { get; set; }
        public string[] AttributeIds { get; set; }
    }

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

            Songs = new ObservableCollection<Song>();
            LoadSongsExternal = new DelegateCommand(OnLoadSongsExternalExecute);
            SaveSongs = new DelegateCommand(OnSaveSongExecute);
            LoadsSongsFromFile = new DelegateCommand(OnLoadSongsFromFileExecute);
        }

        // First test implementation
        // ToDo: Central Manager for Loading and Saving data and build relations between!

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

                // Feature for future => Merge (if something edited on StreamerSonglist)

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
                    var song = serializer.Deserialize(file, typeof(Song)) as Song;
                    Songs.Add(song);
                }
            }
            Songs = new ObservableCollection<Song>(Songs.OrderBy(song => song.Title));
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
            var builder = new UriBuilder($"{_httpClient.BaseAddress}v1/streamers/2557/songs");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["size"] = size.ToString();
            query["current"] = current.ToString();
            builder.Query = query.ToString();

            return builder.Uri;
        }

        private ObservableCollection<Song> _songs;
        public ObservableCollection<Song> Songs 
        {
            get => _songs;
            set => SetProperty(ref _songs, value);
        }

        public DelegateCommand LoadSongsExternal { get; set; }
        public DelegateCommand SaveSongs { get; set; }
        public DelegateCommand LoadsSongsFromFile { get; set; }
    }
}
