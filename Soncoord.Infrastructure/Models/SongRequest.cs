using Soncoord.Infrastructure.Interfaces;
using System;

namespace Soncoord.Infrastructure.Models
{
    public class SongRequest : ISongRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime CreatedAt { get; set; }
        public string[] AttributeIds { get; set; }
    }
}
