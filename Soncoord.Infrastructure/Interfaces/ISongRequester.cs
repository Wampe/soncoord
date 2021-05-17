namespace Soncoord.Infrastructure.Interfaces
{
    public interface ISongRequester
    {
        string Id { get; set; }
        string Name { get; set; }
        string Note { get; set; }
        double Amount { get; set; }
        string Source { get; set; }
        bool InChat { get; set; }
    }
}
