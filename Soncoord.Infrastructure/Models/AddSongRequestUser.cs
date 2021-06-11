namespace Soncoord.Infrastructure.Models
{
    public class AddSongRequestUser : IAddSongRequestUser
    {
        public double Amount { get; set; }
        public string Name { get; set; }
    }
}
