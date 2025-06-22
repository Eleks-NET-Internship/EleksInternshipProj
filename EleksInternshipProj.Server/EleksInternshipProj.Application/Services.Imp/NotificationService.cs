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

        public async Task<IEnumerable<Notification>> GetNotificationsAsync(long userId)
        {
            IEnumerable<Notification> notificationsRaw = await _notificationRepository.GetUserNotificationsAsync(userId);
            return notificationsRaw;
        }

        public async Task SendSpaceNotification(long? exludedId, SpaceAdminNotificationDTO notif)
        {
            if (notif.SpaceId == null)
                throw new ArgumentNullException(nameof(notif));
            Notification notification = new Notification
            {
                Id = 0,
                SpaceId = notif.SpaceId.Value,
                NotificationType = NotificationType.SpaceAdminMessage,
                Title = notif.Title,
                Message = notif.Message,
            };

            await _notificationRepository.AddNotificationAsync(notification);
            _notificationDeliveryService.SendGeneralToSpaceAsync(notif, exludedId);
        }
    }
}
