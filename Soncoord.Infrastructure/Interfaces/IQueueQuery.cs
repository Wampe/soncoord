namespace Soncoord.Infrastructure.Interfaces
{
    public interface IQueueQuery
    {
        IQueue[] List { get; set; }
        object Status { get; set; }
    }
}
