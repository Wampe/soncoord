using Soncoord.Infrastructure.Interfaces;

namespace Soncoord.Infrastructure.Models
{
    public class SongRequester : ISongRequester
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public double Amount { get; set; }
        public string Source { get; set; }
        public bool InChat { get; set; }
    }
}
