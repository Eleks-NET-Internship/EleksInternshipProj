using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.DTOs
{
    public class CreateMarkerDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public long SpaceId { get; set; }
    }
}
