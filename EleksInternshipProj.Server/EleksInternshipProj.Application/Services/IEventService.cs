
using System.Collections.Generic;
using System.Threading.Tasks;
using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;


namespace EleksInternshipProj.Application.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventWithMarkersDto>> GetAllAsync();
        Task<EventWithMarkersDto?> GetByIdAsync(long id);
        Task<Event?> AddAsync(CreateEventDto dto);
        Task<bool> UpdateAsync(UpdateEventDto dto);
        Task<bool> DeleteAsync(long id);

       // Task<EventWithMarkersDto?> GetEventWithMarkersAsync(long eventId);
    }
}
