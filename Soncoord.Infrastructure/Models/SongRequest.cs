using Soncoord.Infrastructure.Interfaces;
using System;

namespace Soncoord.Infrastructure.Models
{
    public class SongRequest : ISongRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime CreatedAt { get; set; }
        public int[] AttributeIds { get; set; }
    }
}
