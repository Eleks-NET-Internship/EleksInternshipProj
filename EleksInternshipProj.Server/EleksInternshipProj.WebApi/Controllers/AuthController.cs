using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using System.Security.Claims;

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
                token = await _authService.LoginAsync(request);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(new { accessToken = token});
        }

        // Google auth
        [HttpGet("login/google")]
        public IActionResult GoogleLogin([FromQuery] string returnUrl = "/")
        {
            AuthenticationProperties properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(GoogleLoginCallback), new { returnUrl })
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("login/google/callback")]
        public async Task<IActionResult> GoogleLoginCallback([FromQuery] string returnUrl)
        {
            AuthenticateResult authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return Unauthorized();

            ClaimsPrincipal claims = authenticateResult.Principal;

            string token = await _authService.GoogleLoginAsync(claims);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!Url.IsLocalUrl(returnUrl))
                returnUrl = "/";

            return Redirect($"https://localhost:4200/auth-callback?{Uri.EscapeDataString(returnUrl)}&accessToken={Uri.EscapeDataString(token)}");
        }
    }
}
