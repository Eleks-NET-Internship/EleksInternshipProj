using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Mappers;
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
                Name = name
            };
            return await _spaceRepository.AddAsync(space, userId);
        }
        
        public async Task<Space?> AddSpaceAsync(SpaceDtoShort spaceDto)
        {
            return await _spaceRepository.AddAsync(spaceDto.ToEntity());
        }

        public async Task<bool> AddUserToSpaceAsync(UserSpaceDto userSpaceDto)
        {
            var newSpace = await _spaceRepository.AddToAsync(userSpaceDto.SpaceId, userSpaceDto.UserId, userSpaceDto.RoleId);
            return newSpace != null;
        }

        public async Task<bool> DeleteSpaceAsync(long spaceId)
        {
            return await _spaceRepository.DeleteAsync(spaceId);
        }

        public async Task<IEnumerable<SpaceDto>> GetSpacesAsync(long userId)
        {
            var allSpaces = (await _spaceRepository.GetByUserAsync(userId)).ToList();
            return allSpaces.Select(s => s.ToDto());
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
