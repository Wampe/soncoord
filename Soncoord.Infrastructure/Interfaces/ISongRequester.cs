namespace Soncoord.Infrastructure.Interfaces
{
    public interface ISongRequester
    {
        int Id { get; set; }
        string Name { get; set; }
        string Note { get; set; }
        double Amount { get; set; }
        string Source { get; set; }
        bool InChat { get; set; }
    }
}
