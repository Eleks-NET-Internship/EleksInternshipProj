namespace EleksInternshipProj.Domain.Abstractions
{
    public interface IUserSpaceRepository
    {
        Task<string> GetUserRoleAsync(long userId, long spaceId);

        Task<bool> IsUserOfRoleAsync(long userId, long spaceId, string role);
    }
}
