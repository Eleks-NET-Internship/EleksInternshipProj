﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MarkerController : ControllerBase
    {
        private readonly IMarkerService _markerService;

        public MarkerController(IMarkerService markerService)
        {
            _markerService = markerService;
        }

        [HttpGet]
        [Route("by-space/{spaceId:long}")]
        public async Task<ActionResult<IEnumerable<MarkerDto>>> GetAllBySpaceId(long spaceId)
        {
            try
            {
                var markers = await _markerService.GetAllBySpaceIdAsync(spaceId);
                return Ok(markers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<MarkerDto?>> GetById(long id)
        {
            try
            {
                var marker = await _markerService.GetByIdAsync(id);
                return Ok(marker);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMarkerDto dto)
        {
            try
            {
                var created = await _markerService.AddAsync(dto);
                return Ok(new { message = "Marker created successfully.", data = created });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update/{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] MarkerDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { message = "ID mismatch." });

                await _markerService.UpdateAsync(dto);
                return Ok(new { message = "Marker updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _markerService.DeleteAsync(id);
                return Ok(new { message = "Marker deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("by-event/{eventId:long}")]
        public async Task<IActionResult> GetAllByEventId(long eventId)
        {
            try
            {
                var markers = await _markerService.GetAllByEventIdAsync(eventId);
                return Ok(new { data = markers });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("add-to-event")]
        public async Task<IActionResult> AddToEvent(long eventId, long markerId)
        {
            try
            {
                var result = await _markerService.AddMarkerToEventAsync(eventId, markerId);
                return Ok(new { message = "Marker added to event successfully.", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("remove-from-event")]
        public async Task<IActionResult> RemoveFromEvent(long eventId, long markerId)
        {
            try
            {
                var result = await _markerService.RemoveMarkerFromEventAsync(eventId, markerId);
                return Ok(new { message = "Marker removed from event successfully.", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
