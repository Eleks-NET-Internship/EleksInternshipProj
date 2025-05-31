using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions
{
    internal interface ITimetableRepository
    {
        Task<Timetable?> GetBySpaceAsync(long spaceId);
        Task<Timetable?> UpdateAsync(Timetable entity);
    }
}
