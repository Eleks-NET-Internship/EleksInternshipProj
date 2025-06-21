using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Mappers;
using EleksInternshipProj.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EleksInternshipProj.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TimeTableController : ControllerBase
{
    private readonly ITimetableService _timetableService;

    public TimeTableController(ITimetableService timetableService)
    {
        _timetableService = timetableService;
    }

    [HttpGet("space/{spaceId:long}")]
    public async Task<ActionResult<TimetableDto>> GetBySpace(long spaceId)
    {
        var timetable = await _timetableService.GetBySpaceAsync(spaceId);
        if (timetable == null)
            return NotFound();
        
        var dto = timetable.ToDto();
        return Ok(dto);
    }

    [HttpPut]
    public async Task<ActionResult<TimetableDto>> Update([FromBody] TimetableDto dto)
    {
        var updated = await _timetableService.UpdateAsync(dto);
        if (updated == null)
            return NotFound();
        return Ok(updated.ToDto());
    }
}