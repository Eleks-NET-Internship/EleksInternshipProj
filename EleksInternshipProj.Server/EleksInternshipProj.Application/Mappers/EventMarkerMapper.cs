using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class EventMarkerMapper
{
    public static EventMarkerDto ToDto(this EventMarker entity)
    {
        return new EventMarkerDto
        {
            Id = entity.Id,
            EventId = entity.EventId,
            MarkerId = entity.MarkerId,
            Marker = entity.Marker.ToDto(),
        };
    }

    public static EventMarker ToEntity(this EventMarkerDto dto)
    {
        return new EventMarker
        {
            Id = dto.Id,
            EventId = dto.EventId,
            MarkerId = dto.MarkerId,
            Marker = dto.Marker.ToEntity(),
        };
    }
}