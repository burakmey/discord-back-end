namespace discord_back_end.ControllersFeatures.AuthFeatures.Register
{
    public class RegisterRequest
    {
        [Required, EmailAddress] public required string Email { get; set; }
        [Required] public required string UserName { get; set; }
        [Required] public required string DisplayName { get; set; }
        [Required, MinLength(6, ErrorMessage = "Less than 6 character!")] public required string Password { get; set; }
        [Required, Compare("Password")] public required string ConfirmPassword { get; set; }
    }
}
