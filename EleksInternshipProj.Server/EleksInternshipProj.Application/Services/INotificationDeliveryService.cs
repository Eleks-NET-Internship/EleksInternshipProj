using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface INotificationDeliveryService
    {
        public Task SendReminderToSpaceAsync(DeadlineNotificationDTO notification);
        public Task SendGeneralToSpaceAsync(SpaceAdminNotificationDTO notification, long? excludedId = null);
        public Task AddUserToSpaceGroupAsync(long userId, long spaceId);
    }
}
