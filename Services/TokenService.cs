using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace discord_back_end.Services
{
    public class TokenService(IConfiguration configuration)
    {
        readonly IConfiguration configuration = configuration;
        public Token CreateAccessToken(int expirationMinute)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["SECURITY_KEY"] ?? throw new Exception("SECURITY_KEY not found!")));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new(
                audience: configuration["Token:Audience"],
                issuer: configuration["Token:Issuer"],
                expires: DateTime.UtcNow.AddSeconds(expirationMinute),
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler securityTokenHandler = new();
            Token token = new()
            {
                AccessToken = securityTokenHandler.WriteToken(securityToken),
                Expiration = DateTime.UtcNow.AddSeconds(expirationMinute),
                RefreshToken = CreateRefreshToken()
            };
            return token;
        }
        public string CreateRefreshToken()
        {
            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            return Convert.ToBase64String(numbers);
        }
    }
}
