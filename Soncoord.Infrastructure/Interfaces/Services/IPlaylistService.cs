using Soncoord.Infrastructure.Events;
using System;
using System.Collections.Generic;

namespace Soncoord.Infrastructure.Interfaces.Services
{
    public interface IPlaylistService
    {
        event EventHandler AddedToPlaylist;
        event EventHandler<RemovedSongFromPlaylistArgs> RemovedFromPlaylist;
        void Add(ISong song);
        void Remove(ISong song, bool isSongPlayed);
        bool Contains(ISong song);
        bool IsPlaylistEmpty();
        ISong GetNextSong();
        ISongSetting GetSongSettings(ISong song);
        IEnumerable<ISong> GetPlaylist();
    }
}
