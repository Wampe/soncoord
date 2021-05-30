using Soncoord.Infrastructure.Interfaces;
using System;

namespace Soncoord.Infrastructure.Events
{
    public class RemovedSongFromPlaylistArgs : EventArgs
    {
        public ISong Song{ get; set; }
        public bool IsSongPlayed { get; set; }
    }
}
