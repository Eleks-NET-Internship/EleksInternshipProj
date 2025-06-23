using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EleksInternshipProj.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

   

    [HttpGet("space/{spaceId}")]
    public async Task<IActionResult> GetAllBySpaceId(long spaceId)
    {
        var notes = await _noteService.GetNotesBySpaceIdAsync(spaceId);
        return Ok(notes);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var note = await _noteService.GetNoteByIdAsync(id);
        return note == null ? NotFound() : Ok(note);
    }

    [HttpGet("by-event/{eventId}")]
    public async Task<IActionResult> GetByEventId(long eventId)
    {
        var notes = await _noteService.GetNotesByEventIdAsync(eventId);
        return Ok(notes);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NoteDto dto)
    {
        await _noteService.CreateNoteAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] NoteDto dto)
    {
        if (id != dto.Id) return BadRequest();
        await _noteService.UpdateNoteAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _noteService.DeleteNoteAsync(id);
        return Ok();
    }
}
