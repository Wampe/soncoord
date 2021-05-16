using Prism.Commands;
using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using System.Collections.Generic;

namespace Soncoord.Player.ViewModels
{
    public class PlaylistViewModel : BindableBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistViewModel(IPlaylistService playlistService)
        {
            _playlistService = playlistService;

            RemoveFromPlaylist = new DelegateCommand<ISong>(OnRemoveFromPlaylistExecute);
            PlaylistSongs = playlistService.GetPlaylist();
        }

        public DelegateCommand<ISong> RemoveFromPlaylist { get; set; }
        public IEnumerable<ISong> PlaylistSongs { get; set; }

        private void OnRemoveFromPlaylistExecute(ISong song)
        {
            _playlistService.Remove(song);
        }
    }
}
