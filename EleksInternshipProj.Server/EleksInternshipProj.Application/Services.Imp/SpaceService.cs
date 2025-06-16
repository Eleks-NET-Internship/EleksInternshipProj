using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class SpaceService : ISpaceService
    {
        private readonly ISpaceRepository _spaceRepository;
        private readonly IUserRepository _userRepository;

        public SpaceService(ISpaceRepository spaceRepository, IUserRepository userRepository)
        {
            _spaceRepository = spaceRepository;
            _userRepository = userRepository;
        }

        public async Task<Space?> AddSpaceAsync(long userId, string name)
        {
            var space = new Space
            {
                Id = 0,
                Name = name
            };
            return await _spaceRepository.AddAsync(space, userId);
        }

        public async Task<bool> AddUserToSpaceAsync(long spaceId, string userName)
        {
            var user = await _userRepository.GetByNameAsync(userName);
            var space = await _spaceRepository.GetByIdAsync(spaceId);
            if (space == null || user == null) return false;

            var newSpace = await _spaceRepository.AddToAsync(space, user.Id);
            return newSpace != null;
        }

        public async Task<bool> DeleteSpaceAsync(long spaceId)
        {
            return await _spaceRepository.DeleteAsync(spaceId);
        }

        public async Task<(IEnumerable<Space>, int)> GetSpacesAsync(long userId)
        {
            var allSpaces = (await _spaceRepository.GetByUserAsync(userId)).ToList();
            return (allSpaces, allSpaces.Count);
        }

        public async Task<Space?> RenameSpaceAsync(long spaceId, string newName)
        {
            var space = await _spaceRepository.GetByIdAsync(spaceId);
            if (space == null) return null;

            space.Name = newName;
            return await _spaceRepository.UpdateAsync(space);
        }
    }
}
