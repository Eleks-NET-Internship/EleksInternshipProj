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
        private readonly INotificationDeliveryService _notificationDeliveryService;

        public SpaceService(ISpaceRepository spaceRepository, IUserRepository userRepository, INotificationDeliveryService notificationDeliveryService)
        {
            _spaceRepository = spaceRepository;
            _userRepository = userRepository;
            _notificationDeliveryService = notificationDeliveryService;
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
            Space? res = await _spaceRepository.AddAsync(spaceDto.ToEntity());
            if (res != null)
            {
                if (spaceDto.UserSpaces != null)
                {
                    foreach (var userSpaceDto in spaceDto.UserSpaces)
                    {
                        await _notificationDeliveryService.AddUserToSpaceGroupAsync(userSpaceDto.UserId, res.Id);
                    }
                }
            }
            return res;
        }

        public async Task<UserSpace?> AddUserToSpaceAsync(long spaceId, string username)
        {
            var user = await _userRepository.GetByNameAsync(username);

            if (user == null)
            {
                return null; // User not found
            }

            var userSpaceDto = new UserSpaceDto
            {
                SpaceId = spaceId,
                UserId = user.Id,
                RoleId = 2
            };

            var newUserSpace = await _spaceRepository.AddToAsync(userSpaceDto.SpaceId, userSpaceDto.UserId, userSpaceDto.RoleId);
            await _notificationDeliveryService.AddUserToSpaceGroupAsync(user.Id, spaceId);
            return newUserSpace;
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
