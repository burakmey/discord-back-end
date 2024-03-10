namespace discord_back_end.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required] public required string Email { get; set; }
        [Required] public required string UserName { get; set; }
        [Required] public required string DisplayName { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        [Required] public DateTime RegisteredAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Server>? OwnedServers { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<ChatGroupMember>? ChatGroups { get; set; }
        public ICollection<ServerMembership>? JoinedServers { get; set; }
    }
}
