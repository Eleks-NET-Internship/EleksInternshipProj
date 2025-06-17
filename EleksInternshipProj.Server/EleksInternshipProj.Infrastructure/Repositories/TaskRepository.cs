using Microsoft.EntityFrameworkCore;

using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;

namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly NavchaykoDbContext _context;

        public TaskRepository(NavchaykoDbContext context)
        {
            _context = context;
        }
        public async Task<TaskModel> AddAsync(TaskModel task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<IEnumerable<TaskModel>> GetAllByEventIdAsync(long eventId)
        {
            return await _context.Tasks
                .Include(t => t.Status) 
                .Include(t => t.Event)
                .Where(t => t.EventId == eventId)
                .OrderBy(t => t.EventTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetAllBySpaceIdAsync(long spaceId)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Event)
                .Where(t => t.Event.SpaceId == spaceId)
                .OrderBy(t => t.EventTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetAllByStatusIdAsync(long spaceId, long statusId)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Event)
                .Where(t => t.StatusId == statusId && t.Event.SpaceId == spaceId)
                .OrderBy(t => t.EventTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Models.TaskStatus>> GetAllStatusesAsync()
        {
            return await _context.TaskStatuses.ToListAsync();
        }

        public async Task<TaskModel?> GetByIdAsync(long id)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Event)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateAsync(TaskModel task)
        {
            _context.Tasks.Update(task);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> UpdateStatusAsync(long taskId, long newStatusId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
                return false;

            task.StatusId = newStatusId;
            _context.Tasks.Update(task);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<IEnumerable<TaskModel>> GetByTimePeriodAsync(DateTime begin, DateTime end)
        {
            IEnumerable<TaskModel> tasks = await _context.Tasks
                .Where(task => task.EventTime > begin && task.EventTime < end)
                .ToListAsync();
            return tasks;
        }
    }
}
