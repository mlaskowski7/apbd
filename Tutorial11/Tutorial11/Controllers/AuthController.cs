using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutorial11.Contracts.Request;
using Tutorial11.Services;
using Tutorial11.Utils;

namespace Tutorial11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            await authService.RegisterUserAsync(registerRequestDto);
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var tokens = await authService.LoginUserAsync(loginRequestDto);
            if (tokens is null)
            {
                return Unauthorized();
            }

            return Ok(new { AccessToken = tokens.Value.accessToken, RefreshToken = tokens.Value.refreshToken });
        }

        [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenReq)
        {
            var tokens = await authService.RefreshTokenAsync(refreshTokenReq.RefreshToken);
            if (tokens is null)
            {
                return Unauthorized();
            }
            return Ok(new { AccessToken = tokens.Value.accessToken, RefreshToken = tokens.Value.refreshToken });
        }

        [Authorize]
        [HttpGet("authenticated")]
        public IActionResult Authenticated()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("anon")]
        public IActionResult Anonymous()
        {
            return Ok();
        }
    }
}
