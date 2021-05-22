using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Soncoord.Business.Provider
{
    public class Streamer
    {
        public bool RequestsActive { get; set; }
    }

    public class StreamerSonglistService : IStreamerSonglistService
    {
        private readonly HttpClient _httpClient;
        private IStreamerSonglistUser User;

        public StreamerSonglistService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.streamersonglist.com")
            };
        }

        public void SetUser(IStreamerSonglistUser user, string token)
        {
            User = user;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ICollection<ISong>> GetSongs()
        {
            return await LoadSongs(100, 0, new Collection<ISong>());
        }

        public void SongRequestStatus(bool value)
        {
            SetSongRequestStatus(value);
        }
       
        public async Task<ICollection<QueueSongRequest>> GetSongRequests()
        {
            return await LoadSongRequests();
        }

        public void SetSongAsPlayed(QueueSongRequest song)
        {
            SongAsPlayed(song.Id);
        }

        public void AddSongToQueue(ISong song)
        {
            throw new NotImplementedException();
        }

        public void MoveSongOnTheTopOfQueue(QueueSongRequest song)
        {
            throw new NotImplementedException();
        }

        private async void SetSongRequestStatus(bool value)
        {
            var json = JsonConvert.SerializeObject(new Streamer
            {
                RequestsActive = value
            }, 
            new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });

            var response = await _httpClient.PutAsync(
                $"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }

        private async Task<ICollection<ISong>> LoadSongs(int size, int current, ICollection<ISong> collection)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}/songs");
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
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}/queue");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var songQuery = JsonConvert.DeserializeObject<QueueQuery>(result);

            return songQuery.List;
        }

        private async void SongAsPlayed(string requestId)
        {
            var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}/queue/{requestId}/played", null);
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }
    }
}
