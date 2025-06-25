using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Infrastructure.Repositories;
using EleksInternshipProj.Infrastructure.Services;

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
        private readonly IUserSpaceRepository _userSpaceService;
        private readonly IEmailService _emailService;
        public SpaceNotificationController(INotificationService notificationService, ISpaceService spaceService, ISpaceAuthService spaceAuthService, IUserSpaceRepository userSpaceService, IEmailService emailService)
        {
            _notificationService = notificationService;
            _spaceService = spaceService;
            _spaceAuthService = spaceAuthService;
            _userSpaceService = userSpaceService;
            _emailService = emailService;
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

                notif.SpaceId = spaceId;
                await _notificationService.SendSpaceNotification(id, notif);

                // тут має бути виклик email
                var space = await _spaceService.GetByIdAsync(notif.SpaceId.Value);
                var users = await _userSpaceService.GetUsersBySpaceId(notif.SpaceId.Value);
                foreach (var user in users)
                {
                    await _emailService.SendEmailNotificationToSpace(user.Email, space?.Name, notif);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }
    }
}