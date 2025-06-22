using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class NotificationService : INotificationService
    {
        public INotificationRepository _notificationRepository;
        public INotificationDeliveryService _notificationDeliveryService;

        public NotificationService(INotificationRepository notificationRepository, INotificationDeliveryService notificationDeliveryService)
        {
            _notificationRepository = notificationRepository;
            _notificationDeliveryService = notificationDeliveryService;
        }

        public async Task<IEnumerable<DeadlineNotificationDTO>> GetNotificationsAsync(long userId)
        {
            IEnumerable<Notification> notificationsRaw = await _notificationRepository.GetUserNotificationsAsync(userId);
            IEnumerable<DeadlineNotificationDTO> notifications = notificationsRaw
                .Select(notif => new DeadlineNotificationDTO
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

        public async Task SendSpaceNotification(long? exludedId, SpaceAdminNotificationDTO notif)
        {
            Notification notification = new Notification
            {
                Id = 0,
                SpaceId = notif.SpaceId,
                RelatedType = "spaceAdminMessage",
                RelatedId = 0,
                Title = notif.Title,
                Message = notif.Message,
                DeadlineAt = DateTime.MinValue, // works, but ugly, should change db
                SentBefore = 0
            };

            await _notificationRepository.AddNotificationAsync(notification);
            _notificationDeliveryService.SendGeneralToSpace(notif, exludedId);
        }
    }
}
