using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetSpaceNotificationsAsync(long spaceId);

        Task<IEnumerable<Notification>> GetUserNotificationsAsync(long spaceId);

        Task AddNotificationAsync(Notification notification);

        Task<bool> ExitstForRelatedAsync(string relatedType, long relatedId);
        
        Task DeleteRelatedAsync(string type, long id);
    }
}
