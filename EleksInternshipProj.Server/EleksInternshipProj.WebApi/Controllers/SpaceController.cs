using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Repositories;
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
        public async Task<IActionResult> GetSpaces(long userId)
        {
            var (spaces, totalCount) = await _spaceService.GetSpacesAsync(userId);

            var result = new
            {
                TotalCount = totalCount,
                Items = spaces.Select(s => new SpaceDto { Id = s.Id, Name = s.Name })
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddSpace(long userId, string spaceName)
        {
            var space = await _spaceService.AddSpaceAsync(userId, spaceName);
            if (space == null) return BadRequest("Failed to create space.");

            return Ok(new SpaceDto { Id = space.Id, Name = space.Name });
        }

        [HttpPost]
        [Route("add-to-space")]
        public async Task<IActionResult> AddToSpace(long spaceId, string userName)
        {
            var result = await _spaceService.AddUserToSpaceAsync(spaceId, userName);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{spaceId:long}")]
        public async Task<IActionResult> DeleteSpace(long spaceId)
        {
            var success = await _spaceService.DeleteSpaceAsync(spaceId);
            return success ? NoContent() : NotFound();
        }

        [HttpPost]
        [Route("rename/{spaceId:long}")]
        public async Task<IActionResult> RenameSpace(long spaceId, string newName)
        {
            var space = await _spaceService.RenameSpaceAsync(spaceId, newName);
            if (space == null) return NotFound();

            return Ok(new SpaceDto { Id = space.Id, Name = space.Name });
        }
    }
}
