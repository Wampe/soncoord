using Newtonsoft.Json;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Soncoord.Business.Player
{
    public class PlaylistService : IPlaylistService
    {
        private ObservableCollection<ISong> _playlist { get; set; }
        
        public PlaylistService()
        {
            _playlist = new ObservableCollection<ISong>();
        }

        public event EventHandler AddedToPlaylist;
        public event EventHandler RemovedFromPlaylist;

        public void Add(ISong song)
        {
            _playlist.Add(song);
            AddedToPlaylist?.Invoke(this, null);
        }

        public bool Contains(ISong song)
        {
            return _playlist.Contains(song);
        }

        public IEnumerable<ISong> GetPlaylist()
        {
            return _playlist;
        }

        public void Remove(ISong song)
        {
            _playlist.Remove(song);
            RemovedFromPlaylist?.Invoke(this, null);
        }

        public bool IsPlaylistEmpty()
        {
            return !_playlist.Any();
        }

        public ISong GetNextSong()
        {
            if (IsPlaylistEmpty())
            {
                return null;
            }

            return _playlist.FirstOrDefault();
        }

        public ISongSetting GetSongSettings(ISong song)
        {
            var fileToOpen = $"{Globals.SongSettingsPath}\\{song.Id}.json";
            if (File.Exists(fileToOpen))
            {
                ISongSetting setting;
                using (var file = File.OpenText(fileToOpen))
                {
                    var serializer = new JsonSerializer();
                    setting = serializer.Deserialize(file, typeof(SongSetting)) as ISongSetting;
                }

                return setting;
            }

            return new SongSetting();
        }
    }
}
