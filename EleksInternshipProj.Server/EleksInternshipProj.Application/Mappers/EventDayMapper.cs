using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class EventDayMapper
{
    public static EventDayDto ToDto(this EventDay entity)
    {
        return new EventDayDto
        {
            Id = entity.Id,
            DayId = entity.DayId,
            EventId = entity.EventId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Event = entity.Event.ToDto(),
        };
    }

    public static EventDay ToEntity(this EventDayDto dto)
    {
        return new EventDay
        {
            Id = dto.Id,
            DayId = dto.DayId,
            EventId = dto.EventId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Event = dto.Event!.ToEntity(),
        };
    }
}