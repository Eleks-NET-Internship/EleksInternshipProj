using Microsoft.EntityFrameworkCore;

using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NavchaykoDbContext _context;

        public NotificationRepository(NavchaykoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetSpaceNotificationsAsync(long spaceId)
        {
            return await _context.Notifications
                .Where(notification => notification.SpaceId == spaceId)
                .ToListAsync();
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExitstForRelatedAsync(string relatedType, long relatedId)
        {
            return await _context.Notifications
                 .AnyAsync(notification => notification.RelatedType == relatedType
                 && notification.RelatedId == relatedId);
        }
    }
}
