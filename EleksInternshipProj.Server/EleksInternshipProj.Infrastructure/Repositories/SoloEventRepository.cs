using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class SoloEventRepository : ISoloEventRepository
    {
        private readonly NavchaykoDbContext _context;
        public SoloEventRepository(NavchaykoDbContext context)
        {
            _context = context;
        }
        public async Task<SoloEvent?> AddAsync(SoloEvent entity)
        {
            await _context.SoloEvents.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.SoloEvents.FindAsync(id);
            if (entity == null)
                return false;

            _context.SoloEvents.Remove(entity);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<IEnumerable<SoloEvent>> GetAllBySpaceIdAsync(long spaceId)
        {
            return await _context.SoloEvents
                .AsNoTracking()
                .Where(se => se.Event.SpaceId == spaceId)
                 .Include(se => se.Event)
                     .ThenInclude(e => e.EventMarkers)
                     .ThenInclude(em => em.Marker)
                     .ToListAsync();
        }

        public async Task<SoloEvent?> GetByIdAsync(long id)
        {
            return await _context.SoloEvents
                .AsNoTracking()
                 .Include(se => se.Event)
                     .ThenInclude(e => e.EventMarkers)
                     .ThenInclude(em => em.Marker)
                    .FirstOrDefaultAsync(se => se.Id == id);
        }

        public async Task<bool> UpdateAsync(SoloEvent entity)
        {
            _context.SoloEvents.Update(entity);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
