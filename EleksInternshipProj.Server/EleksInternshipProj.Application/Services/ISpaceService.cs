using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface ISpaceService
    {
        public Task<IEnumerable<SpaceDto>> GetSpacesAsync(long userId);
        public Task<Space?> AddSpaceAsync(long userId, string name);
        public Task<Space?> AddSpaceAsync(SpaceDtoShort spaceDto);
        public Task<bool> AddUserToSpaceAsync(UserSpaceDto userSpaceDto);
        public Task<bool> DeleteSpaceAsync(long spaceId);
        public Task<Space?> RenameSpaceAsync(long spaceId, string newName);
    }
}
