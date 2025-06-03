using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class EventDayRepository : IEventDayRepository
    {
        private readonly NavchaykoDbContext _context;
        public EventDayRepository(NavchaykoDbContext context)
        {
            _context = context;
        }
        public async Task<EventDay?> AddAsync(EventDay entity)
        {
            try
            {
                var eventDay = new EventDay
                {
                    Event = entity.Event,
                    EventId = entity.EventId,
                    Day = entity.Day,
                    DayId = entity.DayId,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime
                };

                await _context.EventDays.AddAsync(eventDay);
                await _context.SaveChangesAsync();

                return eventDay;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var eventDay = await _context.EventDays.FindAsync(id);
            if (eventDay == null)
                return false;

            _context.EventDays.Remove(eventDay);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<EventDay?> UpdateAsync(EventDay entity)
        {
            var existing = await _context.EventDays.FindAsync(entity.Id);
            if (existing == null)
                return null;

            existing.Event = entity.Event;
            existing.EventId = entity.EventId;
            existing.Day = entity.Day;
            existing.DayId = entity.DayId;
            existing.StartTime = entity.StartTime;
            existing.EndTime = entity.EndTime;

            _context.EventDays.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
