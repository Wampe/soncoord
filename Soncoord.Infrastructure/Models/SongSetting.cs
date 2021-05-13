using Prism.Mvvm;
using Soncoord.Infrastructure.Interfaces;

namespace Soncoord.Infrastructure.Models
{
    public class SongSetting : BindableBase, ISongSetting
    {
        private string _clickTrackPath;
        public string ClickTrackPath
        { 
            get => _clickTrackPath;
            set => SetProperty(ref _clickTrackPath, value);
        }

        private string _musicTrackPath;
        public string MusicTrackPath
        { 
            get => _musicTrackPath;
            set => SetProperty(ref _musicTrackPath, value);
        }
    }
}
