using discord_back_end.ControllersFeatures.AuthFeatures.Login;
using discord_back_end.ControllersFeatures.AuthFeatures.Register;
using discord_back_end.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace discord_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly DataContext dataContext;
        readonly TokenService tokenService;

        public AuthController(DataContext dataContext, TokenService tokenService)
        {
            this.dataContext = dataContext;
            this.tokenService = tokenService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (user == null)
                return BadRequest("User not found!");
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Incorrect!");
            Token token = tokenService.CreateAccessToken(1);
            LoginResponse loginResponse = new(token, user);
            return Ok(loginResponse);
        }

        static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (dataContext.Users.Any(user => user.Email == request.Email))
                return BadRequest("Email already exists.");
            if (dataContext.Users.Any(user => user.UserName == request.UserName))
                return BadRequest("UserName already exists.");
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                DisplayName = request.DisplayName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RegisteredAt = DateTime.Now,
            };
            dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();
            return Ok("User created!");
        }

        static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
