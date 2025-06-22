using Microsoft.AspNetCore.SignalR;

using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Hubs;
using EleksInternshipProj.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EleksInternshipProj.Infrastructure.Services
{
    public class SignalRDeliveryService : INotificationDeliveryService
    {
        IHubContext<NotificationHub> _hubContext;

        public SignalRDeliveryService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void SendToGroup(Notification notification, long? excludedId = null)
        {
            NotificationDTO dto = new NotificationDTO
            {
                Title = notification.Title,
                Message = notification.Message,
                RelatedType = notification.RelatedType,
                RelatedId = notification.RelatedId,
                SpaceId = notification.SpaceId,
                SentAt = notification.SentAt,
                DeadlineAt = notification.DeadlineAt,
                Read = notification.Read
            };

            await _hubContext.Clients
                    .Group($"space-{notification.SpaceId}")
                    .SendAsync("ReceiveNotification", dto);

        }
    }
}
