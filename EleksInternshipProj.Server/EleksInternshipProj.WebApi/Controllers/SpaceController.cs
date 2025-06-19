using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EleksInternshipProj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SpaceController : ControllerBase
    {
        private readonly ISpaceService _spaceService;

        public SpaceController(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetSpaces()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("No id");
            }

            var (spaces, totalCount) = await _spaceService.GetSpacesAsync(long.Parse(userId));

            var result = new
            {
                TotalCount = totalCount,
                Items = spaces.Select(s => new SpaceDto { Id = s.Id, Name = s.Name })
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSpace([FromBody]string spaceName)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("No id");
            }

            var space = await _spaceService.AddSpaceAsync(long.Parse(userId), spaceName);
            if (space == null) return BadRequest("Failed to create space.");

            return Ok(new SpaceDto { Id = space.Id, Name = space.Name });
        }

        [HttpPost]
        [Route("{spaceId:long}")]
        public async Task<IActionResult> AddToSpace(long spaceId, [FromBody] string userName)
        {
            var result = await _spaceService.AddUserToSpaceAsync(spaceId, userName);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{spaceId:long}")]
        public async Task<IActionResult> DeleteSpace(long spaceId)
        {
            var success = await _spaceService.DeleteSpaceAsync(spaceId);
            return success ? NoContent() : NotFound();
        }

        [HttpPatch]
        [Route("{spaceId:long}")]
        public async Task<IActionResult> RenameSpace(long spaceId, [FromBody] string newName)
        {
            var space = await _spaceService.RenameSpaceAsync(spaceId, newName);
            if (space == null) return NotFound();

            return Ok(new SpaceDto { Id = space.Id, Name = space.Name });
        }
    }
}
