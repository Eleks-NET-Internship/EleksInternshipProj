using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly NavchaykoDbContext _context;
        public SpaceRepository(NavchaykoDbContext context)
        {
            _context = context;
        }
        public async Task<Space?> AddAsync(Space newSpace)
        {
            try
            {
                var space = new Space
                {
                    Name = newSpace.Name,
                    UserSpaces = newSpace.UserSpaces
                };

                await _context.Spaces.AddAsync(space);
                await _context.SaveChangesAsync();

                return space;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var space = await _context.Spaces.FindAsync(id);
            if (space == null)
                return false;

            _context.Spaces.Remove(space);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<Space?> GetByIdAsync(long id)
        {
            return await _context.Spaces
                .Include(s => s.UserSpaces)
                .Include(s => s.Markers)
                .Include(s => s.Timetable)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Space>> GetByUserAsync(long userId)
        {
            return await _context.UserSpaces
                .Where(us => us.UserId == userId)
                .Select(us => us.Space)
                .Include(s => s.UserSpaces)
                .Include(s => s.Markers)
                .Include(s => s.Timetable)
                .ToListAsync();
        }

        public async Task<Space?> UpdateAsync(Space space)
        {
            var existing = await _context.Spaces.FindAsync(space.Id);
            if (existing == null)
                return null;

            existing.Name = space.Name;

            _context.Spaces.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
