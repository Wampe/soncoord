using System;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface ISongRequest
    {
        int Id { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        DateTime CreatedAt { get; set; }
        int[] AttributeIds { get; set; }
    }
}
