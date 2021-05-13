using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;

namespace Soncoord.SongManager.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SongListViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        public SongListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Songs = new ObservableCollection<ISong>();
            SongsViewSource = new CollectionViewSource
            {
                Source = Songs
            };
            SongsViewSource.SortDescriptions.Add(new SortDescription("Artist", ListSortDirection.Ascending));
            SongsViewSource.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            SongsViewSource.Filter += SongsViewSourceFilter;

            ResetFilter = new DelegateCommand(OnResetFilterExecute, OnResetFilterCanExecute)
                .ObservesProperty(() => FilterText);

            LoadSongsFromFiles();
        }

        public DelegateCommand ResetFilter { get; set; }
        internal ObservableCollection<ISong> Songs { get; set; }
        internal CollectionViewSource SongsViewSource { get; set; }
        internal ISong PreviousSelectedSong { get; set; }

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

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

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
            e.Accepted = string.IsNullOrWhiteSpace(_filterText)
                || song.Artist.ToLower().Contains(_filterText.ToLower())
                || song.Title.ToLower().Contains(_filterText.ToLower());
        }

        private void LoadSongsFromFiles()
        {
            var files = Directory.GetFiles(Globals.SongsPath);
            if (files != null && files.Length > 0)
            {
                Songs.Clear();

                foreach (var item in files)
                {
                    using (var file = File.OpenText(item))
                    {
                        var serializer = new JsonSerializer();
                        var song = serializer.Deserialize(file, typeof(Song)) as ISong;
                        Songs.Add(song);
                    }
                }

                SelectedSong = SongsView.OfType<ISong>().FirstOrDefault();
            }
        }
    }
}
