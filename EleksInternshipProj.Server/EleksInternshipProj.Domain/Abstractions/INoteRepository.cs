using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions;

public interface INoteRepository
{
   
    Task<List<Note>> GetBySpaceIdAsync(long spaceId);
    Task<Note?> GetByIdAsync(long id);
    Task<List<Note>> GetByEventIdAsync(long eventId);

    Task AddAsync(Note note);
    Task UpdateAsync(Note note);
    Task DeleteAsync(long id);

}
