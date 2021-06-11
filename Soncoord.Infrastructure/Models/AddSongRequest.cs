using Soncoord.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Soncoord.Infrastructure.Models
{
    public class AddSongRequest : IAddSongRequest
    {
        public string SongId { get; set; }
        public ICollection<IAddSongRequestUser> Requests { get; set; }
        public string Note { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowFirstPosition { get; set; }
    }
}
