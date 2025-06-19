using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EleksInternshipProj.Application.Mappers;

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
        public async Task<ActionResult<IEnumerable<SpaceDto>>> GetSpaces()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("No id");
            }

            var spaces = await _spaceService.GetSpacesAsync(long.Parse(userId));
            return Ok(spaces);
        }

        [HttpPost]
        public async Task<ActionResult<SpaceDto>> AddSpace([FromBody]string spaceName)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("No id");
            }

            var space = await _spaceService.AddSpaceAsync(long.Parse(userId), spaceName);
            if (space == null) return BadRequest("Failed to create space.");

            return Ok(space.ToDto());
        }

        [HttpPost]
        [Route("{spaceId:long}")]
        public async Task<ActionResult<UserSpaceDto>> AddToSpace(long spaceId, [FromRoute] long userId, [FromRoute] long roleId)
        {
            var userSpaceDto = new UserSpaceDto
            {
                SpaceId = spaceId,
                UserId = userId,
                RoleId = roleId
            };
            
            var result = await _spaceService.AddUserToSpaceAsync(userSpaceDto);

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
