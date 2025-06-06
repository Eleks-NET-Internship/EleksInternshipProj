using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.DTOs
{
    public class CreateUpdateSoloEventDto
    {
        public long Id { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime EventTime { get; set; }
    }
}
