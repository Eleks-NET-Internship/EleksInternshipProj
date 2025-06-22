using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class UserSpaceRepository : IUserSpaceRepository
    {
        private readonly NavchaykoDbContext _context;

        public UserSpaceRepository(NavchaykoDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetUserRoleAsync(long userId, long spaceId)
        {

            string? roleName = await _context.UserSpaces
                .Where(us => us.UserId == userId && us.SpaceId == spaceId)
                .Select(us => us.Role.Name)
                .FirstOrDefaultAsync();

            if (roleName == null)
            {
                throw new InvalidOperationException($"User {userId} isn't a member of {spaceId} space");
            }
            return roleName;
        }

        public async Task<bool> IsUserOfRoleAsync(long userId, long spaceId, string role)
        {
            string roleName = await GetUserRoleAsync(userId, spaceId);
            return roleName == role;
        }
    }
}
