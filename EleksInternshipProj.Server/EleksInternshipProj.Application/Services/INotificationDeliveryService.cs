using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface INotificationDeliveryService
    {
        public void SendReminderToSpace(DeadlineNotificationDTO notification);
        public void SendGeneralToSpace(SpaceAdminNotificationDTO notification, long? excludedId = null);
    }
}
