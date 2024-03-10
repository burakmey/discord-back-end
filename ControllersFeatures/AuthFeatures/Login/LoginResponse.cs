namespace discord_back_end.ControllersFeatures.AuthFeatures.Login
{
    public class LoginResponse(Token token)
    {
        public string AccessToken { get; set; } = token.AccessToken;
        public string RefreshToken { get; set; } = token.RefreshToken;
    }
}