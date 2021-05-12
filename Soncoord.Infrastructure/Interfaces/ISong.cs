using System;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface ISong
    {
        string Id { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        DateTime CreatedAt { get; set; }
        bool Active { get; set; }
        double MinAmount { get; set; }
        DateTime? LastPlayed { get; set; }
        int TimesPlayed { get; set; }
        int NumQueued { get; set; }
        string[] AttributeIds { get; set; }
    }
}
