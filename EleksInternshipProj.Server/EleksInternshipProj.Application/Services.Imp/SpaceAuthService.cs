using EleksInternshipProj.Domain.Abstractions;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class SpaceAuthService : ISpaceAuthService
    {
        private readonly IUserSpaceRepository _userSpaceRepository;

        public SpaceAuthService(IUserSpaceRepository userSpaceRepository)
        {
            _userSpaceRepository = userSpaceRepository;
        }

        public async Task<string> GetUserRoleAsync(long userId, long spaceId)
        {
            return await _userSpaceRepository.GetUserRoleAsync(userId, spaceId);
        }

        public async Task<bool> IsUserAdminAsync(long userId, long spaceId)
        {
            return await IsUserOfRoleAsync(userId, spaceId, "Адміністратор");
        }

        private async Task<bool> IsUserOfRoleAsync(long userId, long spaceId, string role)
        {
            return await _userSpaceRepository.IsUserOfRoleAsync(userId, spaceId, role);
        }
    }
}
