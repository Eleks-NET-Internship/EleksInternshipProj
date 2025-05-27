using EleksInternshipProj.Domain.Abstractions;
using Task = System.Threading.Tasks.Task;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
