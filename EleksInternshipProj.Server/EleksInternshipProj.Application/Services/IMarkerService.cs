using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services
{
    public interface IMarkerService
    {
        Task<MarkerDto?> GetByIdAsync(long id);
        Task<IEnumerable<MarkerDto>> GetAllBySpaceIdAsync(long spaceId);
        Task<IEnumerable<MarkerDto>> GetAllByEventIdAsync(long eventId);
        Task<MarkerDto> AddAsync(CreateMarkerDto dto);
        Task<bool> UpdateAsync(MarkerDto dto);
        Task<bool> DeleteAsync(long id);

      
        Task<bool> AddMarkerToEventAsync(long eventId, long markerId);
        Task<bool> RemoveMarkerFromEventAsync(long eventId, long markerId);
    }
}
