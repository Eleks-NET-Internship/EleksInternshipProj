using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetSpaceNotificationsAsync(long spaceId);

        Task AddNotificationAsync(Notification notification);
    }
}
