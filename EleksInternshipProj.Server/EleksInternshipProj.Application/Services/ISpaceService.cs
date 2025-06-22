using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface ISpaceService
    {
        public Task<IEnumerable<SpaceDto>> GetSpacesAsync(long userId);
        public Task<IEnumerable<SpaceRenameDto>> GetSpacesWhereAdminAsync(long userId);
        public Task<Space?> AddSpaceAsync(long userId, string name);
        public Task<Space?> AddSpaceAsync(SpaceDtoShort spaceDto);
        public Task<UserSpace?> AddUserToSpaceAsync(long spaceId, string username);
        public Task<bool> DeleteSpaceAsync(long spaceId);
        public Task<Space?> RenameSpaceAsync(long spaceId, string newName);
    }
}
