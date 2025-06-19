using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class UserSpaceMapper
{
    public static UserSpaceDto ToDto(this UserSpace entity)
    {
        return new UserSpaceDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            SpaceId = entity.SpaceId,
            RoleId = entity.RoleId,
            Role = entity.Role.ToDto(),
            User = entity.User?.ToDto()
        };
    }

    public static UserSpace ToEntity(this UserSpaceDto dto)
    {
        return new UserSpace
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SpaceId = dto.SpaceId,
            RoleId = dto.RoleId,
            Role =  dto.Role!.ToEntity(),
            User = dto.User?.ToEntity()
        };
    }
}