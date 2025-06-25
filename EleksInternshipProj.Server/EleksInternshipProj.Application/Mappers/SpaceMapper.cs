using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class SpaceMapper
{
    public static SpaceDto ToDto(this Space entity)
    {
        return new SpaceDto
        {
            Id = entity.Id,
            Name = entity.Name,
            UserSpaces = entity.UserSpaces?.Select(UserSpaceMapper.ToDto).ToList() ?? new List<UserSpaceDto>(),
            Timetable = entity.Timetable.ToDtoShort()
        };
    }
    
    public static SpaceDtoShort ToDtoShort(this Space entity)
    {
        return new SpaceDtoShort
        {
            Id = entity.Id,
            Name = entity.Name,
            UserSpaces = entity.UserSpaces?.Select(UserSpaceMapper.ToDtoShort).ToList() ?? new List<UserSpaceDtoShort>(),
            Timetable = entity.Timetable.ToDtoShort()
        };
    }

    public static Space ToEntity(this SpaceDto dto)
    {
        return new Space
        {
            Id = dto.Id,
            Name = dto.Name,
            UserSpaces = dto.UserSpaces?.Select(UserSpaceMapper.ToEntity).ToList() ?? new List<UserSpace>(),
            Timetable = dto.Timetable.ToEntity(),
        };
    }
    
    public static Space ToEntity(this SpaceDtoShort dto)
    {
        return new Space
        {
            Id = dto.Id,
            Name = dto.Name,
            UserSpaces = dto.UserSpaces?.Select(UserSpaceMapper.ToEntity).ToList() ?? new List<UserSpace>(),
            Timetable = dto.Timetable.ToEntity(),
        };
    }
}