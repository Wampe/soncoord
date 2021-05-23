using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soncoord.Infrastructure.Interfaces.Services
{
    public interface IStreamerSonglistService
    {
        event EventHandler UserChanged;
        bool IsAuthorized { get; }
        void SetUser(IStreamerSonglistUser user, string token);
        Task<IStreamerQueueSettings> GetQueueSettingsAsync();
        Task SetQueueSettingsAsync(IStreamerQueueSettings settings);
        Task<ICollection<ISong>> GetSongsAsync();
        Task<ICollection<QueueSongRequest>> GetSongRequestsAsync();
        Task SetSongAsPlayedAsync(QueueSongRequest request);
        Task RemoveSongFromQueue(QueueSongRequest request);
    }
}
