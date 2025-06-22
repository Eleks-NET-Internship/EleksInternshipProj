using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface INotificationDeliveryService
    {
        public void SendToGroup(Notification notification, long? excludedId = null);
    }
}
