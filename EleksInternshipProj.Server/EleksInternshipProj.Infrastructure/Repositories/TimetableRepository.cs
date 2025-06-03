using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly NavchaykoDbContext _context;
        public TimetableRepository(NavchaykoDbContext context)
        {
            _context = context;
        }
        public async Task<Timetable?> GetBySpaceAsync(long spaceId)
        {
            return await _context.Timetables
                .Include(t => t.Space)
                .Include(t  => t.Days)
                .FirstOrDefaultAsync(t => t.SpaceId == spaceId);
        }

        public async Task<Timetable?> UpdateAsync(Timetable entity)
        {
            var existing = await _context.Timetables.FindAsync(entity.Id);
            if (existing == null)
                return null;

            existing.Space = entity.Space;
            existing.SpaceId = entity.SpaceId;

            _context.Timetables.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
