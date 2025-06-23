using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface INotificationDeliveryService
    {
        public void SendReminderToSpaceAsync(DeadlineNotificationDTO notification);
        public void SendGeneralToSpaceAsync(SpaceAdminNotificationDTO notification, long? excludedId = null);
        public Task AddUserToSpaceGroupAsync(long userId, long spaceId);
    }
}
