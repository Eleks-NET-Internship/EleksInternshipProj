using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;

namespace EleksInternshipProj.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Local auth
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await _authService.RegisterAsync(request);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            string token;

            try
            {
                token = await _authService.ValidateUser(request);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(new { accessToken = token});
        }

        // Google auth
        [HttpPost("login/google")]
        public IActionResult GoogleLogin([FromQuery] string returnUrl = "/")
        {
            AuthenticationProperties properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(GoogleLoginCallback), new { returnUrl })
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpPost("login/google/callback")]
        public async Task<IActionResult> GoogleLoginCallback([FromQuery] string returnUrl)
        {
            AuthenticateResult authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return Unauthorized();

            var claims = authenticateResult.Principal;

            string token = "";

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok( new { accessToken=token});
        }
    }
}
