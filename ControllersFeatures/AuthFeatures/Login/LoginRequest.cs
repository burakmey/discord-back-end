namespace discord_back_end.ControllersFeatures.AuthFeatures.Login
{
    public class LoginRequest
    {
        [Required, EmailAddress] public required string Email { get; set; }
        [Required] public required string Password { get; set; }
    }
}