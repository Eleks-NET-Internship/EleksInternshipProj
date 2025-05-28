using EleksInternshipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EleksInternshipProj.Application.DTOs
{
    public class CreateEventDto
    {
        public string Name { get; set; } = null!;
    }
    public class UpdateEventDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class EventWithMarkersDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<MarkerDto> Markers { get; set; } = Enumerable.Empty<MarkerDto>();
    }
}
