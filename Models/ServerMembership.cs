namespace discord_back_end.Models
{
    public class ServerMembership
    {
        public int ServerId { get; set; }
        public int UserId { get; set; }
        [Required] public DateTime JoinedAt { get; set; }
        public Server? Server { get; set; }
        public User? User { get; set; }
    }
}
