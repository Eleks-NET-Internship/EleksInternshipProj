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
    
    public class EventDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<EventMarkerDto> Markers { get; set; } = Enumerable.Empty<EventMarkerDto>();
    }

    public class EventMarkerDto
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        
        public long MarkerId { get; set; }
        
        public MarkerDto Marker { get; set; } = null!;
    }
}
