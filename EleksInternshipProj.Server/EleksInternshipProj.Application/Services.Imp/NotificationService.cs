using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class NotificationService : INotificationService
    {
        public INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotificationsAsync(long userId)
        {
            IEnumerable<Notification> notificationsRaw = await _notificationRepository.GetUserNotificationsAsync(userId);
            IEnumerable<NotificationDTO> notifications = notificationsRaw
                .Select(notif => new NotificationDTO
                {
                    Title = notif.Title,
                    Message = notif.Message,
                    RelatedType = notif.RelatedType,
                    RelatedId = notif.RelatedId,
                    SpaceId = notif.SpaceId,
                    SentAt = notif.SentAt,
                    DeadlineAt = notif.DeadlineAt,
                    Read = notif.Read

                });
            return notifications;
        }
    }
}
