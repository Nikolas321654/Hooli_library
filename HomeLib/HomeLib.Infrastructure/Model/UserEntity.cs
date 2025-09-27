    namespace HomeLib.Infrastructure.Model;

    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public virtual ICollection<UserPlaylistsEntity> UserPlaylist { get; set; } = new List<UserPlaylistsEntity>(); 
    }