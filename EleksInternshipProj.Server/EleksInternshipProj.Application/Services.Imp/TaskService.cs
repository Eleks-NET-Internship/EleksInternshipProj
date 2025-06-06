using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Mappers;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskModel> AddAsync(TaskDto dto)
        {
            var task = TaskMapper.ToModel(dto);
            task.Id = 0;
            return await _taskRepository.AddAsync(task);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _taskRepository.DeleteAsync(id);
            if (!result)
                throw new ArgumentException($"Task with ID {id} was not found for deletion.", nameof(id));

            return result;
        }

        public async Task<bool> UpdateAsync(TaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(dto.Id);
            if (task == null)
                throw new ArgumentException($"Task with ID {dto.Id} was not found for update.");

            task.Name = dto.Name;
            task.EventId = dto.EventId;
            task.StatusId = dto.StatusId;
            task.EventTime = dto.EventTime;
            task.IsDeadline = dto.IsDeadline;
            task.Description = dto.Description;

            return await _taskRepository.UpdateAsync(task);
        }

        public async Task<bool> UpdateStatusAsync(long taskId, long newStatusId)
        {
            return await _taskRepository.UpdateStatusAsync(taskId, newStatusId);
        }

        public async Task<IEnumerable<TaskDto>> GetAllByEventIdAsync(long eventId)
        {
            var tasks = await _taskRepository.GetAllByEventIdAsync(eventId);
            return tasks.Select(TaskMapper.ToDto);
        }

        public async Task<IEnumerable<TaskDto>> GetAllBySpaceIdAsync(long spaceId)
        {
            var tasks = await _taskRepository.GetAllBySpaceIdAsync(spaceId);
            return tasks.Select(TaskMapper.ToDto);
        }

        public async Task<IEnumerable<TaskDto>> GetAllByStatusIdAsync(long spaceId, long statusId)
        {
            var tasks = await _taskRepository.GetAllByStatusIdAsync(spaceId, statusId);
            return tasks.Select(TaskMapper.ToDto);
        }

        public async Task<TaskDto?> GetByIdAsync(long id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task == null ? null : TaskMapper.ToDto(task);
        }

        public async Task<IEnumerable<TaskModelStatusDto>> GetAllStatusesAsync()
        {
            var statuses = await _taskRepository.GetAllStatusesAsync();

            return statuses.Select(s => new TaskModelStatusDto
            {
                Id = s.Id,
                Name = s.Name
            });
        }
    }
}
