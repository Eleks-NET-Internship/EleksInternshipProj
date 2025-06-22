using EleksInternshipProj.Application.DTOs;

namespace EleksInternshipProj.Application.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetNotificationsAsync(long userId);
    }
}
