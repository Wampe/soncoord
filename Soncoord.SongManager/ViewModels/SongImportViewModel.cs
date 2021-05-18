using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Soncoord.Infrastructure.Interfaces.Services;
using System;
using System.Linq;

namespace Soncoord.SongManager.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SongImportViewModel : BindableBase
    {
        private readonly ISongsService _songsService;

        public SongImportViewModel(ISongsService songsService)
        {
            _songsService = songsService;
            _songsService.Imported += SongsImported;
            
            SynchronizeSongs = new DelegateCommand(OnSynchronizeSongsExecute, OnSynchronizeSongsCanExecute)
                .ObservesProperty(() => IsImportActive);
        }

        public DelegateCommand SynchronizeSongs { get; set; }

        private int _importCount;
        public int ImportCount
        {
            get => _importCount;
            set => SetProperty(ref _importCount, value);
        }

        private bool _isImportActive;
        public bool IsImportActive
        {
            get => _isImportActive;
            set => SetProperty(ref _isImportActive, value);
        }

        private bool OnSynchronizeSongsCanExecute()
        {
            return !IsImportActive;
        }

        private void OnSynchronizeSongsExecute()
        {
            IsImportActive = true;
            _songsService.Import();
        }

        private void SongsImported(object sender, EventArgs e)
        {
            ImportCount = _songsService.GetSongs().Count();
            IsImportActive = false;
        }
    }
}
