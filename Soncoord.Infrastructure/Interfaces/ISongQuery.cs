namespace Soncoord.Infrastructure.Interfaces
{
    public interface ISongQuery
    {
        ISong[] Items { get; set; }
        int Total { get; set; }
    }
}
