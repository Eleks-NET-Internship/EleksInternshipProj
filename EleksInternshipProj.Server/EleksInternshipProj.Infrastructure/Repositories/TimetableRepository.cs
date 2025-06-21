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
                    .ThenInclude(d => d.EventDays)
                    .ThenInclude(ed => ed.Event)
                    .ThenInclude(e => e.EventMarkers)
                    .ThenInclude(em => em.Marker)
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
        
        public async Task<Timetable?> GetByIdAsync(long id)
        {
            var timetable = await _context.Timetables
                .Include(t => t.Days)
                    .ThenInclude(d => d.EventDays)
                    .ThenInclude(ed => ed.Event)
                    .ThenInclude(e => e.EventMarkers)
                    .ThenInclude(em => em.Marker)
                .FirstOrDefaultAsync(t => t.Id == id);

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

            var existing = await this.GetByIdAsync(entity.Id);
            if (existing == null)
            {
                _logger.LogWarning($"Fail! Timetable with ID = {entity.Id} not found!");
                return null;
            }

            try
            {
                existing.SpaceId = entity.SpaceId;
                existing.Days = entity.Days;

                foreach (var eventDay in existing.Days.SelectMany(day => day.EventDays))
                {
                    if (eventDay.EventId != 0)
                    {
                        var hasNewMarkers = eventDay.Event.EventMarkers.Any(em => em.Id == 0);
                        if (hasNewMarkers)
                        {
                            foreach (var marker in eventDay.Event.EventMarkers)
                            {
                                if (marker.Id == 0)
                                {
                                    _context.EventMarkers.Add(marker);
                                }
                            }
                        }
                        eventDay.Event = null!;
                    }
                    else
                    {
                        foreach (var eventMarker in eventDay.Event.EventMarkers.Where(eventMarker => eventMarker.MarkerId != 0))
                        {
                            eventMarker.Marker = null!;
                        }
                    }
                }
                
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
