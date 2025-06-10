using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services
{
    public interface ITaskService
    {
        Task<TaskDto?> GetByIdAsync(long id);
        Task<IEnumerable<TaskDto>> GetAllByEventIdAsync(long eventId);
        Task<IEnumerable<TaskDto>> GetAllBySpaceIdAsync(long spaceId);
        Task<IEnumerable<TaskDto>> GetAllByStatusIdAsync(long spaceId, long statusId);
        Task<TaskModel> AddAsync(TaskDto dto);
        Task<bool> UpdateAsync(TaskDto dto);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateStatusAsync(long taskId, long newStatusId);
        Task<IEnumerable<TaskModelStatusDto>> GetAllStatusesAsync();
    }
}
