using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Interfaces.Services;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Soncoord.SongManager.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SongListViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly ISongsService _songsService;

        public SongListViewModel(IRegionManager regionManager, ISongsService songsService)
        {
            _regionManager = regionManager;
            _songsService = songsService;
            
            SongsViewSource = new CollectionViewSource
            {
                Source = _songsService.GetSongs()
            };
            SongsViewSource.SortDescriptions.Add(new SortDescription("Artist", ListSortDirection.Ascending));
            SongsViewSource.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            SongsViewSource.Filter += SongsViewSourceFilter;

            ResetFilter = new DelegateCommand(OnResetFilterExecute, OnResetFilterCanExecute)
                .ObservesProperty(() => FilterText);

            SelectedSong = SongsView.OfType<ISong>().FirstOrDefault();
        }

        public DelegateCommand ResetFilter { get; set; }
        internal CollectionViewSource SongsViewSource { get; set; }
        internal ISong PreviousSelectedSong { get; set; }

        private bool _filterWithoutFiles;
        public bool FilterWithoutFiles
        {
            get => _filterWithoutFiles;
            set => SetProperty(ref _filterWithoutFiles, value);
        }

        public ICollectionView SongsView
        {
            get => SongsViewSource.View;
        }

        private ISong _selectedSong;
        public ISong SelectedSong
        {
            get => _selectedSong;
            set => SetProperty(ref _selectedSong, value);
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set => SetProperty(ref _filterText, value);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == "FilterWithoutFiles")
            {
                SongsView.Refresh();
            }

            if (args.PropertyName == "FilterText")
            {
                if (string.IsNullOrEmpty(FilterText))
                {
                    SelectedSong = PreviousSelectedSong;
                }
                else
                {
                    if (SelectedSong != null)
                    {
                        PreviousSelectedSong = SelectedSong;
                    }

                    SelectedSong = null;
                }

                SongsView.Refresh();
            }

            if (args.PropertyName == "SelectedSong")
            {
                if (SelectedSong != null)
                {
                    _regionManager.RequestNavigate(
                        Regions.SongDetail,
                        "SongDetail",
                        new NavigationParameters
                        {
                            { "Song", SelectedSong }
                        }
                    );
                }
            }
        }
        
        private bool OnResetFilterCanExecute()
        {
            return !string.IsNullOrEmpty(FilterText);
        }

        private void OnResetFilterExecute()
        {
            SelectedSong = PreviousSelectedSong;
            FilterText = string.Empty;
        }

        private void SongsViewSourceFilter(object sender, FilterEventArgs e)
        {
            var song = e.Item as ISong;
            if (FilterWithoutFiles)
            {
                var setting = _songsService.GetSettings(song);
                e.Accepted = string.IsNullOrEmpty(setting.ClickTrackPath)
                    && string.IsNullOrEmpty(setting.MusicTrackPath);
            }
            else
            {
                e.Accepted = string.IsNullOrWhiteSpace(_filterText)
                    || song.Artist.ToLower().Contains(_filterText.ToLower())
                    || song.Title.ToLower().Contains(_filterText.ToLower());
            }
            
        }
    }
}
