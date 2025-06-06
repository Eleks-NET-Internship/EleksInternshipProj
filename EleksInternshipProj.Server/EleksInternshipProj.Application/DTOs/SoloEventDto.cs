using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.DTOs
{
    public class SoloEventDto
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime EventTime { get; set; }
        public IEnumerable<MarkerDto> Markers { get; set; } = Enumerable.Empty<MarkerDto>();
    }
}
