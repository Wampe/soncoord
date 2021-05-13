using Prism.Mvvm;
using Prism.Regions;

namespace Soncoord.SongManager
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SongManagerViewModel : BindableBase
    {
        public SongManagerViewModel()
        {
        }
    }
}
