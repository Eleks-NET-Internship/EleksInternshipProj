using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class MarkerMapper
{
    public static MarkerDto ToDto(this Marker entity)
    {
        return new MarkerDto
        {
            Id = entity.Id,
            Name = entity.Name,
            SpaceId = entity.SpaceId,
        };
    }

    public static Marker ToEntity(this MarkerDto dto)
    {
        return new Marker
        {
            Id = dto.Id,
            Name = dto.Name,
            SpaceId = dto.SpaceId,
        };
    }
}