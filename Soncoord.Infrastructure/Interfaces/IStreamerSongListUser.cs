namespace Soncoord.Infrastructure.Interfaces
{
    public interface IStreamerSonglistUser
    {
        string UserId { get; set; }
        string Username { get; set; }
        string StreamerId { get; set; }
    }
}
