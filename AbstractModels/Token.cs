namespace discord_back_end.AbstractModels
{
    public class Token
    {
        public required string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public required string RefreshToken { get; set; }
    }
}
