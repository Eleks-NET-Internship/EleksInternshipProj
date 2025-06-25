using Microsoft.AspNetCore.SignalR;

using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Infrastructure.Hubs;
using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Infrastructure.Services
{
    public class SignalRDeliveryService : INotificationDeliveryService
    {
        IHubContext<NotificationHub> _hubContext;

        public SignalRDeliveryService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendReminderToSpaceAsync(DeadlineNotificationDTO notification)
        {
            await _hubContext.Clients
                    .Group($"space-{notification.SpaceId}")
                    .SendAsync("ReceiveReminderNotification", notification);
        }

        public async Task SendGeneralToSpaceAsync(SpaceAdminNotificationDTO notification, long? excludedId = null)
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

        public async Task AddUserToSpaceGroupAsync(long userId, long spaceId)
        {
            var connections = NotificationHub.GetUserConnections(userId);
            foreach (var connectionId in connections)
            {
                await _hubContext.Groups.AddToGroupAsync(connectionId, $"space-{spaceId}");
            }
        }

    }
}
