using Microsoft.AspNetCore.SignalR;

using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Hubs;
using EleksInternshipProj.Application.DTOs;
using Microsoft.IdentityModel.Logging;

namespace EleksInternshipProj.Infrastructure.Services
{
    public class SignalRDeliveryService : INotificationDeliveryService
    {
        IHubContext<NotificationHub> _hubContext;

        public SignalRDeliveryService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void SendReminderToSpace(DeadlineNotificationDTO notification)
        {
            await _hubContext.Clients
                    .Group($"space-{notification.SpaceId}")
                    .SendAsync("ReceiveReminderNotification", notification);
        }

        public async void SendGeneralToSpace(SpaceAdminNotificationDTO notification, long? excludedId = null)
        {
            if (excludedId.HasValue)
            {
                var excludedConnections = NotificationHub.GetUserConnections(excludedId.Value);

                await _hubContext.Clients
                    .GroupExcept($"space-{notification.SpaceId}", excludedConnections)
                    .SendAsync("ReceiveSpaceNotification", notification);
            }
            else
            {
                await _hubContext.Clients
                    .Group($"space-{notification.SpaceId}")
                    .SendAsync("ReceiveSpaceNotification", notification);
            }
        }
    }
}
