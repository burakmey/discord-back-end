namespace discord_back_end.Models
{
    public class ChatGroup
    {
        public int Id { get; set; }
        [Required] public DateTime StartedAt { get; set; }
        public ICollection<ChatGroupMember>? Members { get; set; }

    }
}
