using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class TimetableMapper
{
    public static TimetableDto ToDto(this Timetable entity)
    {
        return new TimetableDto
        {
            Id = entity.Id,
            SpaceId = entity.SpaceId,
            Days = entity.Days?.Select(DayMapper.ToDto).ToList() ?? new List<DayDto>()
        };
    }
    
    public static TimetableDtoShort ToDtoShort(this Timetable entity)
    {
        if (entity == null)
        {
            return null;
        }
        
        return new TimetableDtoShort
        {
            Id = entity.Id,
            SpaceId = entity.SpaceId
        };
    }

    public static Timetable ToEntity(this TimetableDto dto)
    {
        return new Timetable
        {
            Id = dto.Id,
            SpaceId = dto.SpaceId,
            Days = dto.Days?.Select(DayMapper.ToEntity).ToList() ?? new List<Day>()
        };
    }
    
    public static Timetable ToEntity(this TimetableDtoShort? dto)
    {
        if (dto == null)
        {
            return null!;
        }
        
        return new Timetable
        {
            Id = dto.Id,
            SpaceId = dto.SpaceId
        };
    }
}