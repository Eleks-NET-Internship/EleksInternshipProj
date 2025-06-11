using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Mappers;
using EleksInternshipProj.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EleksInternshipProj.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeTableController : ControllerBase
{
    private readonly ITimetableService _timetableService;

    public TimeTableController(ITimetableService timetableService)
    {
        _timetableService = timetableService;
    }

    [HttpGet("space/{spaceId:long}")]
    public async Task<IActionResult> GetBySpace(long spaceId)
    {
        var timetable = await _timetableService.GetBySpaceAsync(spaceId);
        if (timetable == null)
            return NotFound();
        return Ok(timetable.ToDto());
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TimetableDto dto)
    {
        var updated = await _timetableService.UpdateAsync(dto);
        if (updated == null)
            return NotFound();
        return Ok(updated.ToDto());
    }
}