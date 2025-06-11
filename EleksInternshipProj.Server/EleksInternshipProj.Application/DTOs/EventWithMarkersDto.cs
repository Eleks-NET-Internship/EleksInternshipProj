using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EleksInternshipProj.Domain.Models;


namespace EleksInternshipProj.Application.DTOs
{

    public class EventWithMarkersDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long SpaceId { get; set; }
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
