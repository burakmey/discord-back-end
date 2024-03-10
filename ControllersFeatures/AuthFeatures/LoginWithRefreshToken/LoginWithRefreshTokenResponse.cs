namespace discord_back_end.ControllersFeatures.AuthFeatures.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenResponse(Token token)
    {
        public string AccessToken { get; set; } = token.AccessToken;
        public string RefreshToken { get; set; } = token.RefreshToken;
    }
}
