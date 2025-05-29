
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleksInternshipProj.Domain.Models;


namespace EleksInternshipProj.Domain.Abstractions
{
    public interface IMarkerRepository
    {
        Task<Marker?> GetByIdAsync(long id);
        Task<IEnumerable<Marker>> GetAllAsync();
        Task<IEnumerable<Marker>> GetAllByEventIdAsync(long eventId);
        Task<Marker> AddAsync(Marker marker);
        Task<bool> UpdateAsync(Marker marker);
        Task<bool> DeleteAsync(long id);


        Task<bool> AddMarkerToEventAsync(long eventId, long markerId);
        Task<bool> RemoveMarkerFromEventAsync(long eventId, long markerId);
    }


}
