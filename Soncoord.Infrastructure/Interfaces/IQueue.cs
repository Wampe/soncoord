using System;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface IQueue
    {
        string Id { get; set; }
        string Note { get; set; }
        string BotRequestBy { get; set; }
        string NonlistSong { get; set; }
        double DonationAmount { get; set; }
        DateTime CreatedAt { get; set; }
        bool ReadOnly { get; set; }
        ISongRequest Song { get; set; }
        ISongRequester[] Requests { get; set; }
        string SongId { get; set; }
        string StreamerId { get; set; }
        int Position { get; set; }
    }
}
