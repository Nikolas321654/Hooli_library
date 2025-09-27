namespace HomeLib.Infrastructure.Model;

public class UserPlaylistsEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }

    public virtual UserEntity User { get; set; } = null!;
    public virtual ICollection<PlaylistTracksEntity> PlaylistTracks { get; set; } = new List<PlaylistTracksEntity>();
}