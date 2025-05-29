using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.DTOs
{
    public class UpdateEventDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
