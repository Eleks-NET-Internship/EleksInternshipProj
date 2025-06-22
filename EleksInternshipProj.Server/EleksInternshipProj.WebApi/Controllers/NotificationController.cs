using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNotifications()
        {
            IEnumerable<Notification> notifications;

            try
            {
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    return Unauthorized("No id");
                }

                notifications = await _notificationService.GetNotificationsAsync(long.Parse(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(new { data = notifications });
        }
    }
}