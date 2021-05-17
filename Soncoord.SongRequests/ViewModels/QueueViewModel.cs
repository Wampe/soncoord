using Newtonsoft.Json;
using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Soncoord.SongRequests.ViewModels
{
    public class QueueViewModel : BindableBase
    {
        private readonly DispatcherTimer _queueTimer;
        private readonly HttpClient _httpClient;

        public QueueViewModel()
        {
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

            SongRequestQueue = new ObservableCollection<Queue>();

            LoadSongs();
        }

        public ObservableCollection<Queue> SongRequestQueue { get; set; }

        private async void LoadSongs()
        {
            var result = await GetSongs();
            var songQuery = JsonConvert.DeserializeObject<QueueQuery>(result);

            SongRequestQueue.Clear();
            foreach (var item in songQuery.List)
            {
                SongRequestQueue.Add(item);
            }
        }

        private void QueueTimerTicked(object sender, EventArgs e)
        {
            LoadSongs();
        }

        private async Task<string> GetSongs()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/wampe/queue");
            //var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/2557/queue");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
