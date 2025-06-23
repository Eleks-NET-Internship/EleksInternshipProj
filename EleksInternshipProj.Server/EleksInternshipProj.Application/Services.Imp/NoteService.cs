using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    

    public async Task<List<NoteDto>> GetNotesBySpaceIdAsync(long spaceId)
    {
        var notes = await _noteRepository.GetBySpaceIdAsync(spaceId);
        return notes.Select(n => new NoteDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            EventId = n.EventId
        }).ToList();
    }


    public async Task<NoteDto?> GetNoteByIdAsync(long id)
    {
        var note = await _noteRepository.GetByIdAsync(id);
        return note == null ? null : new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            EventId = note.EventId
        };
    }

    public async Task<List<NoteDto>> GetNotesByEventIdAsync(long eventId)
    {
        var notes = await _noteRepository.GetByEventIdAsync(eventId);
        return notes.Select(n => new NoteDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            EventId = n.EventId
        }).ToList();
    }


    public async Task CreateNoteAsync(NoteDto dto)
    {
        var note = new Note
        {
            Title = dto.Title,
            Content = dto.Content,
            EventId = dto.EventId
        };
        await _noteRepository.AddAsync(note);
    }

    public async Task UpdateNoteAsync(NoteDto dto)
    {
        var note = new Note
        {
            Id = dto.Id,
            Title = dto.Title,
            Content = dto.Content,
            EventId = dto.EventId
        };
        await _noteRepository.UpdateAsync(note);
    }

    public async Task DeleteNoteAsync(long id)
    {
        await _noteRepository.DeleteAsync(id);
    }
}
