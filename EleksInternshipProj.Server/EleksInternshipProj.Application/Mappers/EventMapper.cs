using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class EventMapper
{
    public static EventDto ToDto(this Event entity)
    {
        return new EventDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Markers = entity.EventMarkers?.Select(EventMarkerMapper.ToDto).ToList() ?? new List<EventMarkerDto>(),
        };
    }

    public static Event ToEntity(this EventDto dto)
    {
        return new Event
        {
            Id = dto.Id,
            Name = dto.Name,
            EventMarkers = dto.Markers?.Select(EventMarkerMapper.ToEntity).ToList() ?? new List<EventMarker>(),
            SpaceId = dto.SpaceId,
        };
    }
}