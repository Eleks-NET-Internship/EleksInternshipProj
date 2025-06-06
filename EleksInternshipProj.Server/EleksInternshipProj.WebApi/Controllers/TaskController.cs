using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("event/{eventId:long}")]
        public async Task<IActionResult> GetAllByEventId(long eventId)
        {
            try
            {
                var tasks = await _taskService.GetAllByEventIdAsync(eventId);
                return Ok(new { data = tasks });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("space/{spaceId:long}")]
        public async Task<IActionResult> GetAllBySpaceId(long spaceId)
        {
            try
            {
                var tasks = await _taskService.GetAllBySpaceIdAsync(spaceId);
                return Ok(new { data = tasks });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("space/{spaceId:long}/status/{statusId:long}")]
        public async Task<IActionResult> GetAllByStatusId(long spaceId, long statusId)
        {
            try
            {
                var tasks = await _taskService.GetAllByStatusIdAsync(spaceId, statusId);
                return Ok(new { data = tasks });
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
                var task = await _taskService.GetByIdAsync(id);
                if (task == null)
                    return NotFound(new { message = $"Task with ID {id} was not found." });

                return Ok(new { data = task });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] TaskDto dto)
        {
            try
            {
                var created = await _taskService.AddAsync(dto);
                return Ok(new { message = "Task created successfully.", data = created.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update/{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] TaskDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { message = "ID mismatch." });

                await _taskService.UpdateAsync(dto);
                return Ok(new { message = "Task updated successfully." });
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

        [HttpPut]
        [Route("update-status/{id:long}/{newStatusId:long}")]
        public async Task<IActionResult> UpdateStatus(long id, long newStatusId)
        {
            try
            {
                await _taskService.UpdateStatusAsync(id, newStatusId);
                return Ok(new { message = "Task status updated successfully." });
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
                await _taskService.DeleteAsync(id);
                return Ok(new { message = "Task deleted successfully." });
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
        [Route("statuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            try
            {
                var statuses = await _taskService.GetAllStatusesAsync();
                return Ok(new { data = statuses });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
