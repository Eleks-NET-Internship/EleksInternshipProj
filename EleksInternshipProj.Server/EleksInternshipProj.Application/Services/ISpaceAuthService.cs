namespace EleksInternshipProj.Application.Services
{
    public interface ISpaceAuthService
    {
        public Task<string> GetUserRoleAsync(long userId, long spaceId);

        public Task<bool> IsUserAdminAsync(long userId, long spaceId);
    }
}
