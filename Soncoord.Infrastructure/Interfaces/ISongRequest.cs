using System;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface ISongRequest
    {
        string Id { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        DateTime CreatedAt { get; set; }
        string[] AttributeIds { get; set; }
    }
}
