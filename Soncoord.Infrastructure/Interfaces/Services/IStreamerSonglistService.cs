using Soncoord.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soncoord.Infrastructure.Interfaces.Services
{
    public interface IStreamerSonglistService
    {
        void SetUser(IStreamerSonglistUser user, string token);
        void SongRequestStatus(bool value);
        Task<ICollection<ISong>> GetSongs();
        Task<ICollection<QueueSongRequest>> GetSongRequests();
        void SetSongAsPlayed(QueueSongRequest song);
        void AddSongToQueue(ISong song);
        void MoveSongOnTheTopOfQueue(QueueSongRequest song);
    }
}
