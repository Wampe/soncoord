using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Soncoord.Infrastructure;
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
    public class StreamerSonglistService : IStreamerSonglistService
    {
        private readonly HttpClient _httpClient;
        
        public StreamerSonglistService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Globals.SongProviderApiAddress)
            };
        }

        public event EventHandler UserChanged;
        private IStreamerSonglistUser User { get; set; }

        public bool IsAuthorized
        {
            get => User != null;
        }

        public void SetUser(IStreamerSonglistUser user, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return;
            }

            User = user;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            UserChanged?.Invoke(this, null);
        }

        public async Task<ICollection<ISong>> GetSongsAsync()
        {
            return await GetSongs(100, 0, new Collection<ISong>());
        }

        public async Task<ICollection<QueueSongRequest>> GetSongRequestsAsync()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}/queue");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var songQuery = JsonConvert.DeserializeObject<QueueQuery>(result);

            return songQuery.List;
        }

        public async Task SetSongAsPlayedAsync(QueueSongRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}/queue/{request.Id}/played", null);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                //Console.WriteLine("\nException Caught!");
                //Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public async Task RemoveSongFromQueue(QueueSongRequest request)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}/queue/{request.Id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                //Console.WriteLine("\nException Caught!");
                //Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public async Task<IStreamerQueueSettings> GetQueueSettingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}v1/streamers/{User.StreamerId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var queueSettings = JsonConvert.DeserializeObject<StreamerQueueSettings>(result) as IStreamerQueueSettings;

            return queueSettings;
        }

        public async Task SetQueueSettingsAsync(IStreamerQueueSettings settings)
        {
            try
            {
                var json = JsonConvert.SerializeObject(
                   settings,
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
            }
            catch (HttpRequestException e)
            {
                //Console.WriteLine("\nException Caught!");
                //Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private async Task<ICollection<ISong>> GetSongs(int size, int current, ICollection<ISong> collection)
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
                var songs = await GetSongs(size, current + 1, collection);
                collection.Concat(songs);
            }

            return collection;
        }
    }
}
