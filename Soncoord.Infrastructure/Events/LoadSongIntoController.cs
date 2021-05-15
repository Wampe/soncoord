using Prism.Events;
using Soncoord.Infrastructure.Interfaces;

namespace Soncoord.Infrastructure.Events
{
    public class LoadSongIntoControllerParameters
    {
        public ISong Song { get; set; }
        public ISongSetting Settings { get; set; }
    }

    public class LoadSongIntoControllerEvent : PubSubEvent<LoadSongIntoControllerParameters>
    {
    }
}
