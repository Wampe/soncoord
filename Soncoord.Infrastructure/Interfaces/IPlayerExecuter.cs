using System;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface IPlayerExecuter
    {
        event EventHandler<TimeSpan> PositionChanged;
        event EventHandler<TimeSpan> Started;
        event EventHandler Stopped;
        event EventHandler Ended;
        bool IsTrackRunReverted { get; set; }
        void Play(ISongSetting settings);
        void Play(string clickTrack, string songTrack);
        void Stop();
    }
}
