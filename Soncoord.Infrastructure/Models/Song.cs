using Soncoord.Infrastructure.Interfaces;
using System;

namespace Soncoord.Infrastructure.Models
{
    public class Song : ISong
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
        public double MinAmount { get; set; }
        public DateTime? LastPlayed { get; set; }
        public int TimesPlayed { get; set; }
        public int NumQueued { get; set; }
        public int[] AttributeIds { get; set; }
    }
}
