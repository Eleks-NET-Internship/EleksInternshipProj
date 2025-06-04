using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class EventDayRepository : IEventDayRepository
    {
        private readonly NavchaykoDbContext _context;
        private readonly ILogger<EventDayRepository> _logger;

        public EventDayRepository(NavchaykoDbContext context, ILogger<EventDayRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<EventDay?> AddAsync(EventDay entity)
        {
            _logger.LogInformation($"Adding new EventDay with EventId = {entity.EventId} and DayId = {entity.DayId}");

            try
            {
                await _context.EventDays.AddAsync(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Success! EventDay with ID = {entity.Id} added.");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail! Error occurred while adding new EventDay.");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            _logger.LogInformation($"Attempting to delete EventDay with ID = {id}");

            var eventDay = await _context.EventDays.FindAsync(id);
            if (eventDay == null)
            {
                _logger.LogWarning($"Fail! EventDay with ID = {id} not found!");
                return false;
            }

            _context.EventDays.Remove(eventDay);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                _logger.LogInformation($"Success! EventDay with ID = {id} deleted.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Fail! EventDay with ID = {id} not deleted.");
                return false;
            }
        }

        public async Task<EventDay?> UpdateAsync(EventDay entity)
        {
            _logger.LogInformation($"Updating EventDay with ID = {entity.Id}");

            var existing = await _context.EventDays.FindAsync(entity.Id);
            if (existing == null)
            {
                _logger.LogWarning($"Fail! EventDay with ID = {entity.Id} not found!");
                return null;
            }

            try
            {
                existing.Event = entity.Event;
                existing.EventId = entity.EventId;
                existing.Day = entity.Day;
                existing.DayId = entity.DayId;
                existing.StartTime = entity.StartTime;
                existing.EndTime = entity.EndTime;

                _context.EventDays.Update(existing);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Success! EventDay with ID = {existing.Id} updated.");
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail! Error occurred while updating EventDay with ID = {entity.Id}.");
                return null;
            }
        }
    }
}
