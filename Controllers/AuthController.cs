using discord_back_end.ControllersFeatures.AuthFeatures.Login;
using discord_back_end.ControllersFeatures.AuthFeatures.LoginWithRefreshToken;
using discord_back_end.ControllersFeatures.AuthFeatures.Register;
using Microsoft.AspNetCore.Mvc;

namespace discord_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserService userService, TokenService tokenService) : ControllerBase
    {
        readonly UserService userService = userService;
        readonly TokenService tokenService = tokenService;
        readonly int accessTokenSecond = 10;
        readonly int refreshTokenSecond = 5;

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            User? user = await userService.GetUserAsync(request);
            if (user == null)
                return BadRequest("Incorrect email or password!");
            Token token = tokenService.CreateAccessToken(accessTokenSecond);
            await userService.UpdateRefreshTokenAsync(user, token.RefreshToken, token.Expiration, refreshTokenSecond);
            LoginResponse response = new(token);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            RegisterResponse response = await userService.CreateUser(request);
            if (!response.Succeeded)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithRefreshToken(LoginWithRefreshTokenRequest request)
        {
            User? user = await userService.GetUserWithRefreshTokenAsync(request.RefreshToken);
            if (user == null)
                return BadRequest("Invalid refresh token!");
            Token token = tokenService.CreateAccessToken(accessTokenSecond);
            await userService.UpdateRefreshTokenAsync(user, token.RefreshToken, token.Expiration, refreshTokenSecond);
            LoginWithRefreshTokenResponse response = new(token);
            return Ok(response);
        }
    }
}
