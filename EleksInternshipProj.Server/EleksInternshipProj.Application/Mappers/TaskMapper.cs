using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Mappers
{
    public static class TaskMapper
    {
        public static TaskDto ToDto(TaskModel task)
        {
            return new TaskDto
            {
                Id = task.Id,
                EventId = task.EventId,
                StatusId = task.StatusId,
                StatusName = task.Status?.Name,
                Name = task.Name,
                EventTime = task.EventTime,
                IsDeadline = task.IsDeadline,
                Description = task.Description
            };
        }

        public static TaskModel ToModel(TaskDto dto)
        {
            return new TaskModel
            {
                Id = dto.Id,
                EventId = dto.EventId,
                StatusId = dto.StatusId,
                Name = dto.Name,
                EventTime = dto.EventTime,
                IsDeadline = dto.IsDeadline,
                Description = dto.Description
            };
        }

    }
}
