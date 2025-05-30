using Task = System.Threading.Tasks.Task;

using Microsoft.EntityFrameworkCore;

using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NavchaykoDbContext _context;

        public UserRepository(NavchaykoDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAndProviderAsync(string email, string provider)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.AuthProvider == provider);
        }

        public async Task<long> GetIdByEmailAndProviderAsync(string email, string provider)
        {
            return await _context.Users
                .Where(u => u.Email == email && u.AuthProvider == provider)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }
    }
}
