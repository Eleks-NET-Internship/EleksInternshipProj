using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.DTOs
{
    public class MarkerDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public long SpaceId { get; set; }
    }

}
