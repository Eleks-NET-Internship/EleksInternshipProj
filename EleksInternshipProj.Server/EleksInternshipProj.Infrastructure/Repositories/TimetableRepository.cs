using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly NavchaykoDbContext _context;
        private readonly ILogger<TimetableRepository> _logger;
        public TimetableRepository(NavchaykoDbContext context, ILogger<TimetableRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Timetable?> GetBySpaceAsync(long spaceId)
        {
            _logger.LogInformation($"Searching Timetable by Space with ID = {spaceId}");

            var timetable = await _context.Timetables
                .Include(t => t.Space)
                .Include(t => t.Days)
                .FirstOrDefaultAsync(t => t.SpaceId == spaceId);

            if (timetable == null)
            {
                _logger.LogWarning($"Fail! Timetable with ID = {timetable.Id} not found!");
            }
            else
            {
                _logger.LogInformation($"Success! Timetable with ID = { timetable.Id} found.");
            }

            return timetable;
        }

        public async Task<Timetable?> UpdateAsync(Timetable entity)
        {
            _logger.LogInformation($"Updating Timetable with ID = {entity.Id}");

            var existing = await _context.Timetables.FindAsync(entity.Id);
            if (existing == null)
            {
                _logger.LogWarning($"Fail! Timetable with ID = {entity.Id} not found!");
                return null;
            }

            try
            {
                existing.Space = entity.Space;
                existing.SpaceId = entity.SpaceId;

                _context.Timetables.Update(existing);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Success! Timetable with ID = {existing.Id} updated.");

                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail! Something went wrong when trying to update Timetable with ID = {entity.Id}!");
                return null;
            }
        }

    }
}
