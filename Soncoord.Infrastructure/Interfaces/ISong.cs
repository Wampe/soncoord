using System;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface ISong : ISongRequest
    {
        bool Active { get; set; }
        double MinAmount { get; set; }
        DateTime? LastPlayed { get; set; }
        int TimesPlayed { get; set; }
        int NumQueued { get; set; }
    }
}
