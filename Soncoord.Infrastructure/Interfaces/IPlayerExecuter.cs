using System;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface IPlayerExecuter
    {
        event EventHandler<TimeSpan> PositionChanged;
        event EventHandler<TimeSpan> Started;
        bool IsTrackRunReverted { get; set; }
        void Play(ISongSetting settings);
        void Play(string clickTrack, string songTrack);
        void Stop();
    }
}
