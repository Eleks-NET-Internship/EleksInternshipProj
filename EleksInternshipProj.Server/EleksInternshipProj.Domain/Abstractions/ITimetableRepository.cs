using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions
{
    public interface ITimetableRepository
    {
        Task<Timetable?> GetBySpaceAsync(long spaceId);
        Task<Timetable?> GetByIdAsync(long id);
        Task<Timetable?> UpdateAsync(Timetable entity);
    }
}
