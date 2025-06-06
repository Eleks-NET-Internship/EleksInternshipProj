using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services
{
    public interface ISoloEventService
    {
        Task<SoloEvent?> AddAsync(CreateUpdateSoloEventDto dto);
        Task<bool> UpdateAsync(CreateUpdateSoloEventDto dto);
        Task<bool> DeleteAsync(long id);
        Task<SoloEventDto?> GetByIdAsync(long id);
        Task<IEnumerable<SoloEventDto>> GetAllAsync();
    }
}
