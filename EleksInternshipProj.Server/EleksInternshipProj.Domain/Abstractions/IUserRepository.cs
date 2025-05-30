using Task = System.Threading.Tasks.Task;

using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAndProviderAsync(string email, string provider);

        Task<long> GetIdByEmailAndProviderAsync(string email, string provider);

        Task AddUserAsync(User user);
    }
}
