﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.DTOs
{
    public class CreateEventDto
    {
        public string Name { get; set; } = null!;
        public long spaceId { get; set; }
    }
}
