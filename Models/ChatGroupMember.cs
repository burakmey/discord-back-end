namespace discord_back_end.Models
{
    public class ChatGroupMember
    {
        public int ChatGroupId { get; set; }
        public int UserId { get; set; }
        [Required] public DateTime JoinedAt { get; set; }
        public ChatGroup? ChatGroup { get; set; }
        public User? User { get; set; }
    }
}
