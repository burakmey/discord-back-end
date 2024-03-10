using discord_back_end.ControllersFeatures.AuthFeatures.Login;
using discord_back_end.ControllersFeatures.AuthFeatures.Register;
using System.Security.Cryptography;
using System.Text;


namespace discord_back_end.Services
{
    public class UserService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;
        public async Task<RegisterResponse> CreateUser(RegisterRequest request)
        {
            if (dataContext.Users.Any(user => user.Email == request.Email))
                return new() { Succeeded = false, Message = "Email already exists." };
            if (dataContext.Users.Any(user => user.UserName == request.UserName))
                return new() { Succeeded = false, Message = "UserName already exists." };
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
            return new() { Succeeded = true, Message = "User created!" };
        }
        public async Task<User?> GetUserAsync(LoginRequest request)
        {
            User? user = await dataContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }
        public async Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime accessTokenDate, int refreshTokenAdditionalMinute)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenAdditionalMinute);
            dataContext.Users.Update(user);
            await dataContext.SaveChangesAsync();
        }
        public async Task<User?> GetUserWithRefreshTokenAsync(string refreshToken)
        {
            User? user = await dataContext.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
                return user;
            return null;
        }
        static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
