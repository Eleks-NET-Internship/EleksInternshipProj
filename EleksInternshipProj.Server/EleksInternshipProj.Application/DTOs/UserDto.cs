namespace EleksInternshipProj.Application.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}