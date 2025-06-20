using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly NavchaykoDbContext _context;
        private readonly ILogger<SpaceRepository> _logger;

        public SpaceRepository(NavchaykoDbContext context, ILogger<SpaceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Space?> AddAsync(Space space, long userId)
        {
            _logger.LogInformation($"Adding new Space with Name '{space.Name}' for User ID = {userId}");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Spaces.AddAsync(space);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Space with ID {space.Id} successfully created. Assigning to user...");

                var userSpace = new UserSpace
                {
                    SpaceId = space.Id,
                    UserId = userId,
                    RoleId = 1
                };

                await _context.UserSpaces.AddAsync(userSpace);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                _logger.LogInformation($"Success! User ID = {userId} assigned to Space ID = {space.Id}");
                return space;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Fail! Error occurred while adding new Space '{space.Name}' for User ID = {userId}.");
                return null;
            }
        }
        
        public async Task<Space?> AddAsync(Space space)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Spaces.AddAsync(space);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                
                var result = await this.GetByIdAsync(space.Id);
                
                if (result == null)
                {
                    _logger.LogWarning($"Fail! Space '{space.Name}' not created.");
                    return null;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Fail! Error occurred while adding new Space: '{space}'");
                return null;
            }
        }

        public async Task<UserSpace?> AddToAsync(long spaceId, long userId, long roleId)
        {
            _logger.LogInformation($"Adding new User ID = {userId} to Space with Id '{spaceId}'");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userSpace = new UserSpace
                {
                    SpaceId = spaceId,
                    UserId = userId,
                    RoleId = 2
                };

                await _context.UserSpaces.AddAsync(userSpace);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var result = await _context.UserSpaces.Include(us => us.User)
                    .FirstOrDefaultAsync(us => us.SpaceId == spaceId && us.UserId == userId);;
                
                if (result == null)
                {
                    _logger.LogWarning($"Fail! User ID = {userId} not assigned to Space ID = {spaceId}");
                    return null;
                }
                else
                {
                    _logger.LogWarning($"Success! User ID = {userId} assigned to Space ID = {spaceId}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Fail! Error occurred while adding new Space '{spaceId}' for User ID = {userId}.");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            _logger.LogInformation($"Attempting to delete Space with ID: {id}");

            var space = await _context.Spaces.FindAsync(id);
            if (space == null)
            {
                _logger.LogWarning($"Fail! Space with ID {id} not found!");
                return false;
            }

            _context.Spaces.Remove(space);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                _logger.LogInformation($"Success! Space with ID {id} deleted.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Fail! Space with ID {id} not deleted.");
                return false;
            }
        }

        public async Task<Space?> GetByIdAsync(long id)
        {
            _logger.LogInformation($"Searching for Space with ID: {id}");

            var space = await _context.Spaces
                .Include(s => s.UserSpaces).ThenInclude(us => us.User)
                .Include(s => s.Timetable)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (space == null)
                _logger.LogWarning($"Fail! Space with ID {id} not found!");
            else
                _logger.LogInformation($"Success! Space with ID {id} found.");

            return space;
        }

        public async Task<IEnumerable<Space>> GetByUserAsync(long userId)
        {
            _logger.LogInformation($"Fetching Spaces for User with ID = {userId}");

            var spaces = await _context.Spaces
                .Where(s => s.UserSpaces.Any(us => us.UserId == userId))
                .Include(s => s.UserSpaces)
                .ThenInclude(us => us.User)
                .Include(s => s.Timetable)
                .ToListAsync();

            _logger.LogInformation($"Found {spaces.Count} Space(s) for User ID = {userId}");
            return spaces;
        }

        public async Task<Space?> UpdateAsync(Space space)
        {
            _logger.LogInformation($"Updating Space with ID = {space.Id}");

            var existing = await _context.Spaces.FindAsync(space.Id);
            if (existing == null)
            {
                _logger.LogWarning($"Fail! Space with ID = {space.Id} not found!");
                return null;
            }

            try
            {
                existing.Name = space.Name;

                _context.Spaces.Update(existing);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Success! Space with ID = {existing.Id} updated.");
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail! Error occurred while updating Space with ID = {space.Id}.");
                return null;
            }
        }
    }
}
