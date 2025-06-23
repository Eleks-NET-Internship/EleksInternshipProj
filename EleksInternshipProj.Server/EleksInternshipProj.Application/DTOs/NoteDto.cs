namespace EleksInternshipProj.Application.DTOs;

public class NoteDto
{
    public long Id { get; set; }
    public long EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
