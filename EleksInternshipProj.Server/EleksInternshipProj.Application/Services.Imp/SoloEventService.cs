using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class SoloEventService : ISoloEventService
    {
        private readonly ISoloEventRepository _soloEventRepository;
        private readonly IEventRepository _eventRepository;

        public SoloEventService(ISoloEventRepository soloEventRepository, IEventRepository eventRepository)
        {
            _soloEventRepository = soloEventRepository;
            _eventRepository = eventRepository;
        }
        public async Task<SoloEvent?> AddAsync(CreateUpdateSoloEventDto dto)
        {
            var newEvent = new Event
            {
                Id = 0,
                Name = dto.EventName,
                IsSolo = true
            };

            var createdEvent = await _eventRepository.AddAsync(newEvent);

            var soloEvent = new SoloEvent
            {
                EventId = createdEvent.Id,
                EventTime = dto.EventTime
            };

            return await _soloEventRepository.AddAsync(soloEvent);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var soloEvent = await _soloEventRepository.GetByIdAsync(id);
            if (soloEvent == null)
                return false;

            var deletedSolo = await _soloEventRepository.DeleteAsync(id);
            var deletedEvent = await _eventRepository.DeleteAsync(soloEvent.EventId);

            return deletedSolo && deletedEvent;
        }

        public async Task<IEnumerable<SoloEventDto>> GetAllAsync()
        {
            var soloEvents = await _soloEventRepository.GetAllAsync();

            return soloEvents.Select(se => new SoloEventDto
            {
                Id = se.Id,
                EventId = se.EventId,
                EventName = se.Event.Name,
                EventTime = se.EventTime,
                Markers = se.Event.EventMarkers.Select(em => new MarkerDto
                {
                    Id = em.Marker.Id,
                    Name = em.Marker.Name,
                    Type = em.Marker.Type,
                    SpaceId = em.Marker.SpaceId
                }).ToList()
            });
        }

        public async Task<SoloEventDto?> GetByIdAsync(long id)
        {
            var soloEvent = await _soloEventRepository.GetByIdAsync(id);
            if (soloEvent == null)
                return null;

            return new SoloEventDto
            {
                Id = soloEvent.Id,
                EventId = soloEvent.EventId,
                EventName = soloEvent.Event.Name,
                EventTime = soloEvent.EventTime,
                Markers = soloEvent.Event.EventMarkers.Select(em => new MarkerDto
                {
                    Id = em.Marker.Id,
                    Name = em.Marker.Name,
                    Type = em.Marker.Type,
                    SpaceId = em.Marker.SpaceId
                }).ToList()
            };
        }

        public async Task<bool> UpdateAsync(CreateUpdateSoloEventDto dto)
        {
            var soloEvent = await _soloEventRepository.GetByIdAsync(dto.Id);
            if (soloEvent == null)
                throw new ArgumentException($"SoloEvent with ID {dto.Id} not found for update.");

            var eventEntity = await _eventRepository.GetByIdAsync(soloEvent.EventId);
            if (eventEntity == null)
                throw new InvalidOperationException("Related event not found.");

         
            eventEntity.Name = dto.EventName;
            soloEvent.EventTime = dto.EventTime;

            var updatedEvent = await _eventRepository.UpdateAsync(eventEntity);
            var updatedSolo = await _soloEventRepository.UpdateAsync(soloEvent);

            return updatedEvent && updatedSolo;
        }
    }
}
