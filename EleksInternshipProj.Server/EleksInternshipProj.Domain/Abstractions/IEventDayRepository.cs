using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions
{
    internal interface IEventDayRepository
    {
        Task<EventDay?> AddAsync(EventDay entity);
        Task<EventDay?> UpdateAsync(EventDay entity);
        Task<bool> DeleteAsync(long id);
    }
}
