using Task = System.Threading.Tasks.Task;

using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions
{
    // just exemple, this folder for repository interfaces
    public interface IUserRepository
    {
        Task<User?> GetByEmailAndProviderAsync(string email, string provider);

        Task<long> GetIdByEmailAndProviderAsync(string email, string provider);

        Task AddUserAsync(User user);

        Task<User?> GetByIdAsync(long userId);

        Task UpdateUserAsync(User user);
    }
}
