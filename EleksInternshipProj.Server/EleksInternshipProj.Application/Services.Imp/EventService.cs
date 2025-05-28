using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Event?> AddAsync(CreateEventDto dto)
        {
            var newEvent = new Event
            {
                Name = dto.Name
            };

            return await _eventRepository.AddAsync(newEvent);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _eventRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EventWithMarkersDto>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();

            return events.Select(ev => new EventWithMarkersDto
            {
                Id = ev.Id,
                Name = ev.Name,
                Markers = ev.EventMarkers.Select(em => new MarkerDto
                {
                    Id = em.Marker.Id,
                    Name = em.Marker.Name,
                    SpaceId = em.Marker.SpaceId
                }).ToList()
            });
        }

        public async Task<EventWithMarkersDto?> GetByIdAsync(long id)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null) return null;

            return new EventWithMarkersDto
            {
                Id = ev.Id,
                Name = ev.Name,
                Markers = ev.EventMarkers.Select(em => new MarkerDto
                {
                    Id = em.Marker.Id,
                    Name = em.Marker.Name,
                    SpaceId = em.Marker.SpaceId
                }).ToList()
            };
        }

        public async Task<bool> UpdateAsync(UpdateEventDto dto)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(dto.Id);
            if (existingEvent == null)
                return false;

            existingEvent.Name = dto.Name;
            return await _eventRepository.UpdateAsync(existingEvent);
        }

    }
}
