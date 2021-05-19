using Newtonsoft.Json;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Soncoord.Business.Provider
{
    public class StreamerSonglistService : IStreamerSonglistService
    {
        private readonly HttpClient _httpClient;

        public StreamerSonglistService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.streamersonglist.com")
            };
        }

        public async Task<ICollection<ISong>> GetSongs()
        {
            return await LoadSongs(100, 0, new Collection<ISong>());
        }

        public async Task<ICollection<QueueSongRequest>> GetSongRequests()
        {
            return await LoadSongRequests();
        }

        public void SetSongAsPlayed(QueueSongRequest song)
        {
            throw new NotImplementedException();
        }

        public void AddSongToQueue(ISong song)
        {
            throw new NotImplementedException();
        }

        public void MoveSongOnTheTopOfQueue(QueueSongRequest song)
        {
            throw new NotImplementedException();
        }

        private async Task<ICollection<ISong>> LoadSongs(int size, int current, ICollection<ISong> collection)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}v1/streamers/wampe/songs");
            //var builder = new UriBuilder($"{_httpClient.BaseAddress}v1/streamers/2557/songs");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["size"] = size.ToString();
            query["current"] = current.ToString();
            query["current"] = current.ToString();
            builder.Query = query.ToString();

            var response = await _httpClient.GetAsync(builder.Uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
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

        private async Task<ICollection<QueueSongRequest>> LoadSongRequests()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/wampe/queue");
            //var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/2557/queue");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var songQuery = JsonConvert.DeserializeObject<QueueQuery>(result);

            return songQuery.List;
        }
    }
}
