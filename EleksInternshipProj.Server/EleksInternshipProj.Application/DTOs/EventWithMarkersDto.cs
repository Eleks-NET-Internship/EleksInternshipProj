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
        public IEnumerable<MarkerDto> Markers { get; set; } = Enumerable.Empty<MarkerDto>();
    }
}
