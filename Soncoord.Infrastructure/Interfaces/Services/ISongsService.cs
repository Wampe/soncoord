using System;
using System.Collections.Generic;

namespace Soncoord.Infrastructure.Interfaces.Services
{
    public interface ISongsService
    {
        event EventHandler Imported;
        IEnumerable<ISong> GetSongs();
        ISong GetSongById(string songId);
        ISongSetting GetSettings(ISong song);
        void SaveSettings(ISong song, ISongSetting settings);
        void Import();
    }
}
