using EleksInternshipProj.Application.DTOs;

namespace EleksInternshipProj.Application.Services;

public interface INoteService
{
    
    Task<List<NoteDto>> GetNotesBySpaceIdAsync(long spaceId);
    Task<NoteDto?> GetNoteByIdAsync(long id);
    Task<List<NoteDto>> GetNotesByEventIdAsync(long eventId);

    Task CreateNoteAsync(NoteDto noteDto);
    Task UpdateNoteAsync(NoteDto noteDto);
    Task DeleteNoteAsync(long id);
}
