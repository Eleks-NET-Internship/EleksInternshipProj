using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface ISpaceService
    {
        public Task<(IEnumerable<Space>, int)> GetSpacesAsync(long userId);
        public Task<Space?> AddSpaceAsync(long userId, string name);
        public Task<bool> AddUserToSpaceAsync(long spaceId, string userName);
        public Task<bool> DeleteSpaceAsync(long spaceId);
        public Task<Space?> RenameSpaceAsync(long spaceId, string newName);
    }
}
