namespace EleksInternshipProj.Application.DTOs;

public class UserSpaceDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long SpaceId { get; set; }
    public long RoleId { get; set; }
    public RoleDto? Role { get; set; }
    public UserDto? User { get; set; }
}