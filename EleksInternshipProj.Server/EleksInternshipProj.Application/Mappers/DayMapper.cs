using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class DayMapper
{
    public static DayDto ToDto(this Day entity)
    {
        return new DayDto
        {
            Id = entity.Id,
            DayName = entity.DayName,
            TimetableId = entity.TimetableId,
            EventDays = entity.EventDays?.Select(EventDayMapper.ToDto).ToList() ?? new List<EventDayDto>()
        };
    }

    public static Day ToEntity(this DayDto dto)
    {
        return new Day
        {
            Id = dto.Id,
            TimetableId = dto.TimetableId,
            DayName = dto.DayName,
            EventDays = dto.EventDays?.Select(EventDayMapper.ToEntity).ToList() ?? new List<EventDay>(),
        };
    }
}