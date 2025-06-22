using EleksInternshipProj.Application.DTOs;

namespace EleksInternshipProj.Application.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<DeadlineNotificationDTO>> GetNotificationsAsync(long userId);
        Task SendSpaceNotification(long? excludedId, SpaceAdminNotificationDTO notif);
    }
}
