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
        private ObservableCollection<ISong> Playlist { get; set; }
        
        public PlaylistService()
        {
            Playlist = new ObservableCollection<ISong>();
        }

        public event EventHandler AddedToPlaylist;
        public event EventHandler RemovedFromPlaylist;

        public void Add(ISong song)
        {
            Playlist.Add(song);
            AddedToPlaylist?.Invoke(this, null);
        }

        public bool Contains(ISong song)
        {
            return Playlist.Contains(song);
        }

        public IEnumerable<ISong> GetPlaylist()
        {
            return Playlist;
        }

        public void Remove(ISong song)
        {
            Playlist.Remove(song);
            RemovedFromPlaylist?.Invoke(this, null);
        }

        public bool IsPlaylistEmpty()
        {
            return !Playlist.Any();
        }

        public ISong GetNextSong()
        {
            if (IsPlaylistEmpty())
            {
                return null;
            }

            return Playlist.FirstOrDefault();
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
