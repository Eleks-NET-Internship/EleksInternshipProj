using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpaceController : ControllerBase
    {
        private readonly ISpaceService _spaceService;
        public SpaceController(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        private long GetUserId() => long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetSpaces([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            var userId = GetUserId();
            var (spaces, totalCount) = await _spaceService.GetSpacesAsync(userId, currentPage, pageSize);

            var result = new
            {
                TotalCount = totalCount,
                Items = spaces.Select(s => new SpaceDto { Id = s.Id, Name = s.Name })
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddSpace([FromBody] string spaceName)
        {
            var userId = GetUserId();
            var space = await _spaceService.AddSpaceAsync(userId, spaceName);
            if (space == null) return BadRequest("Failed to create space.");

            return Ok(new SpaceDto { Id = space.Id, Name = space.Name });
        }

        [HttpDelete]
        [Route("delete/{spaceId:long}")]
        public async Task<IActionResult> DeleteSpace(long spaceId)
        {
            var success = await _spaceService.DeleteSpaceAsync(spaceId);
            return success ? NoContent() : NotFound();
        }

        [HttpPatch]
        [Route("rename/{spaceId:long}")]
        public async Task<IActionResult> RenameSpace(long spaceId, [FromBody] string newName)
        {
            var space = await _spaceService.RenameSpaceAsync(spaceId, newName);
            if (space == null) return NotFound();

            return Ok(new SpaceDto { Id = space.Id, Name = space.Name });
        }
    }
}
