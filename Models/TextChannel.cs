namespace discord_back_end.Models
{
    public class TextChannel
    {
        public int Id { get; set; }
        [Required] public required string Name { get; set; }
        [ForeignKey(nameof(Server))] public int ServerId { get; set; }
        public Server? Server { get; set; }
    }
}
