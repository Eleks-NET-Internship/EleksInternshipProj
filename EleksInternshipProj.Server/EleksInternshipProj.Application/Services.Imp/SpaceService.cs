using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class SpaceService : ISpaceService
    {
        private readonly ISpaceRepository _spaceRepository;
        public SpaceService(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task<Space?> AddSpaceAsync(long userId, string name)
        {
            var space = new Space { Name = name };
            return await _spaceRepository.AddAsync(space, userId);
        }

        public async Task<bool> DeleteSpaceAsync(long spaceId)
        {
            return await _spaceRepository.DeleteAsync(spaceId);
        }

        public async Task<(IEnumerable<Space>, int)> GetSpacesAsync(long userId, int currentPage, int pageSize)
        {
            var allSpaces = (await _spaceRepository.GetByUserAsync(userId)).ToList();
            var pagedSpaces = allSpaces.Skip((currentPage - 1) * pageSize).Take(pageSize);
            return (pagedSpaces, allSpaces.Count);
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
