using System.Collections.Generic;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface IAddSongRequest
    {
        int SongId { get; set; }
        ICollection<IAddSongRequestUser> Requests { get; set; }
        string Note { get; set; }
        bool AllowUpdate { get; set; }
        bool AllowFirstPosition { get; set; }
    }
}
