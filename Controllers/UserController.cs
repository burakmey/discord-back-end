using discord_back_end.ControllersFeatures.UserFeatures.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace discord_back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        readonly UserService userService = userService;

        [HttpPost("[action]")]
        public async Task<IActionResult> GetUser(GetUserRequest request)
        {
            User? user = await userService.GetUserWithRefreshTokenAsync(request.RefreshToken);
            if (user == null)
                return BadRequest("Invalid refresh token!");
            GetUserResponse response = new(user);
            return Ok(response);
        }

    }
}
