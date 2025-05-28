using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var events = await _eventService.GetAllAsync();
                return Ok(new { data = events });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var ev = await _eventService.GetByIdAsync(id);
                if (ev == null)
                    return NotFound(new { message = $"Event with ID {id} not found." });

                return Ok(new { data = ev });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            try
            {
                var created = await _eventService.AddAsync(dto);
                return Ok(new { message = "Event created successfully.", data = created });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update/{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateEventDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { message = "ID mismatch." });

                var success = await _eventService.UpdateAsync(dto);
                if (!success)
                    return NotFound(new { message = $"Event with ID {id} not found." });

                return Ok(new { message = "Event updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var success = await _eventService.DeleteAsync(id);
                if (!success)
                    return NotFound(new { message = $"Event with ID {id} not found." });

                return Ok(new { message = "Event deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
