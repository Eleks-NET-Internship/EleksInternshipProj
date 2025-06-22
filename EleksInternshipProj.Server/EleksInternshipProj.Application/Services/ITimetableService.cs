using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services;

public interface ITimetableService
{
    Task<Timetable?> GetBySpaceAsync(long spaceId);
    Task<Timetable?> UpdateAsync(TimetableDto dto);
}