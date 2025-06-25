using EleksInternshipProj.Domain.Models;

using TaskStatus = EleksInternshipProj.Domain.Models.TaskStatus;

namespace EleksInternshipProj.Domain.Abstractions
{
    public interface ITaskRepository
    {
        Task<TaskModel?> GetByIdAsync(long id);
        Task<IEnumerable<TaskModel>> GetAllBySpaceIdAsync(long spaceId);
        Task<IEnumerable<TaskModel>> GetAllByEventIdAsync(long eventId);
        Task<IEnumerable<TaskModel>> GetAllByStatusIdAsync(long spaceId, long statusId);
        Task<IEnumerable<TaskStatus>> GetAllStatusesAsync();
        Task<TaskModel> AddAsync(TaskModel task);
        Task<bool> UpdateAsync(TaskModel task);
        Task<bool> UpdateStatusAsync(long taskId, long newStatusId);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<TaskModel>> GetByTimePeriodWithoutNotifAsync(DateTime begin, DateTime end, int sentBeforeMinutes);
    }
}
