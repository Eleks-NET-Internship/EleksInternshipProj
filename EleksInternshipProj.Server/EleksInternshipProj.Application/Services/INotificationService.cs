using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetNotificationsAsync(long userId);
        Task SendSpaceNotification(long? excludedId, SpaceAdminNotificationDTO notif);
    }
}
