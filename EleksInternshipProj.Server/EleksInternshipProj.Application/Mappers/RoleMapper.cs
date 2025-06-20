using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class RoleMapper
{
    public static RoleDto ToDto(this Role? entity)
    {
        if (entity == null)
        {
            return null!;
        }
        
        return new RoleDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static Role? ToEntity(this RoleDto? dto)
    {
        if (dto == null)
        {
            return null;
        }
        
        return new Role
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}