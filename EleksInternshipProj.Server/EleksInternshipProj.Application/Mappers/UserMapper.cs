using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Username = entity.Username,
            Email = entity.Email,
        };
    }

    public static User? ToEntity(this UserDto? dto)
    {
        if (dto == null)
        {
            return null;
        }
        
        return new User
        {
            Id = dto.Id,
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };
    }
}