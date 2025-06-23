using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

public class NoteRepository : INoteRepository
{
    private readonly NavchaykoDbContext _context;

    public NoteRepository(NavchaykoDbContext context)
    {
        _context = context;
    }

    

    public async Task<Note?> GetByIdAsync(long id) =>
        await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

    public async Task<List<Note>> GetByEventIdAsync(long eventId)
    {
        return await _context.Notes
            .Where(n => n.EventId == eventId)
            .ToListAsync();
    }
    public async Task<List<Note>> GetBySpaceIdAsync(long spaceId)
    {
        return await _context.Notes
            .Include(n => n.Event)
            .Where(n => n.Event.SpaceId == spaceId)
            .ToListAsync();
    }


    public async Task AddAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var note = await GetByIdAsync(id);
        if (note != null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}
