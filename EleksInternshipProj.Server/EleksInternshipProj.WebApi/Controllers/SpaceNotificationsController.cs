using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/spaces/{spaceId}/notifications")]
    [Authorize]
    public class SpaceNotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ISpaceService _spaceService;
        private readonly ISpaceAuthService _spaceAuthService;
        public SpaceNotificationController(INotificationService notificationService, ISpaceService spaceService, ISpaceAuthService spaceAuthService)
        {
            _notificationService = notificationService;
            _spaceService = spaceService;
            _spaceAuthService = spaceAuthService;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotificationToSpace(long spaceId, [FromBody] SpaceAdminNotificationDTO notif)
        {
            try
            {
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("No id");
                }

                long id = long.Parse(userId);
                if (!await _spaceAuthService.IsUserAdminAsync(id, spaceId))
                    return Unauthorized("Not admin");

                await _notificationService.SendSpaceNotification(id, notif);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }
    }
}