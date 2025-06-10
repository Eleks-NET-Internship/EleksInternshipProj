using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.DTOs
{
    public class TaskDto
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public long StatusId { get; set; }

        // Optional field for POST/PUT
        public string? StatusName { get; set; }

        public string Name { get; set; } = string.Empty;
        public DateTime EventTime { get; set; }
        public bool IsDeadline { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
