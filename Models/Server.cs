namespace discord_back_end.Models
{
    public class Server
    {
        public int Id { get; set; }
        [Required] public required string Name { get; set; }
        [ForeignKey(nameof(Owner))] public int OwnerId { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
        public User? Owner { get; set; }
        public ICollection<ServerMembership>? Members { get; set; }
        public ICollection<TextChannel>? TextChannels { get; set; }

    }
}
