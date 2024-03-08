namespace discord_back_end.Models
{
    public class Friendship
    {
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public DateTime BeenFriendAt { get; set; }
        public User? User1 { get; set; }
        public User? User2 { get; set; }
    }
}
