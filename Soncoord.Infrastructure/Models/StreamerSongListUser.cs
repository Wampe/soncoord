using Soncoord.Infrastructure.Interfaces;

namespace Soncoord.Infrastructure.Models
{
    public class StreamerSonglistUser : IStreamerSonglistUser
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string StreamerId { get; set; }
    }
}
