using EleksInternshipProj.Domain.Models;
using Task = System.Threading.Tasks.Task;
namespace EleksInternshipProj.Domain.Abstractions
{
    // just example, this folder for repository interfaces
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
