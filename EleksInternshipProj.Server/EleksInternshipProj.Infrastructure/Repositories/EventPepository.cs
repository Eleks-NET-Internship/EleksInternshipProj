using System.Collections.Generic;
using System.Threading.Tasks;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly NavchaykoDbContext _context;
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(NavchaykoDbContext context, ILogger<EventRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Event>> GetAllBySpaceIdAsync(long spaceId)
        {
            return await _context.Events
                .Where(e => e.SpaceId == spaceId && e.IsSolo == false)
                .Include(e => e.EventMarkers)
                    .ThenInclude(em => em.Marker)
                .ToListAsync();
        }


        public async Task<Event?> GetByIdAsync(long id)
        {
            return await _context.Events
               .Where(e => e.IsSolo == false)
              .Include(e => e.EventMarkers)
                  .ThenInclude(em => em.Marker)
              .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event?> AddAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.Events.FindAsync(id);
            if (entity == null)
                return false;

            _context.Events.Remove(entity);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> UpdateAsync(Event entity)
        {
            _context.Events.Update(entity);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

    }
}
