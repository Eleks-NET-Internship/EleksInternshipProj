using System;
using System.Threading.Tasks;
using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UniqueEventsController : ControllerBase
    {
        private readonly ISoloEventService _soloEventService;

        public UniqueEventsController(ISoloEventService soloEventService)
        {
            _soloEventService = soloEventService;
        }

        [HttpGet]
        [Route("all/{spaceId:long}")]
        public async Task<IActionResult> GetAll(long spaceId)
        {
            try
            {
                var soloEvents = await _soloEventService.GetAllBySpaceIdAsync(spaceId);
                return Ok(new { data = soloEvents });
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
                var soloEvent = await _soloEventService.GetByIdAsync(id);
                if (soloEvent == null)
                    return NotFound(new { message = $"SoloEvent with ID {id} not found." });

                return Ok(new { data = soloEvent });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUpdateSoloEventDto dto)
        {
            try
            {
                var created = await _soloEventService.AddAsync(dto);
                return Ok(new { message = "SoloEvent created successfully.", data = new {created.Id, created.EventId } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update/{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] CreateUpdateSoloEventDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { message = "ID mismatch." });

                var updated = await _soloEventService.UpdateAsync(dto);
                return Ok(new { message = "SoloEvent updated successfully.", success = updated });
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
                var result = await _soloEventService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"SoloEvent with ID {id} not found." });

                return Ok(new { message = "SoloEvent deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
