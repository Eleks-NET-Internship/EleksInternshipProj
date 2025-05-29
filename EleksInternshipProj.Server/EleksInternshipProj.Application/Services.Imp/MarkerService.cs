using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;


namespace EleksInternshipProj.Application.Services.Imp
{
    public class MarkerService : IMarkerService
    {
        private readonly IMarkerRepository _markerRepository;

        public MarkerService(IMarkerRepository markerRepository)
        {
            _markerRepository = markerRepository;
        }

        public async Task<MarkerDto> AddAsync(CreateMarkerDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var newMarker = new Marker
            {
                Name = dto.Name,
                Type = dto.Type,
                SpaceId = dto.SpaceId
            };

            var addedMarker = await _markerRepository.AddAsync(newMarker);

            return new MarkerDto
            {
                Id = addedMarker.Id,
                Name = addedMarker.Name,
                Type = addedMarker.Type,
                SpaceId = addedMarker.SpaceId
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _markerRepository.DeleteAsync(id);
            if (!result)
                throw new ArgumentException($"Marker with Id={id} was not found.", nameof(id));

            return result;
        }

        public async Task<IEnumerable<MarkerDto>> GetAllAsync()
        {
            var markers = await _markerRepository.GetAllAsync();

            return markers.Select(m => new MarkerDto
            {
                Id = m.Id,
                Name = m.Name,
                Type = m.Type,
                SpaceId = m.SpaceId
            });
        }

        public async Task<MarkerDto?> GetByIdAsync(long id)
        {
            var marker = await _markerRepository.GetByIdAsync(id);

            if (marker == null)
                throw new ArgumentException($"Marker with Id={id} was not found.", nameof(id));

            return new MarkerDto
            {
                Id = marker.Id,
                Name = marker.Name,
                Type = marker.Type,
                SpaceId = marker.SpaceId
            };
        }

        public async Task<IEnumerable<MarkerDto>> GetAllByEventIdAsync(long eventId)
        {
            var markers = await _markerRepository.GetAllByEventIdAsync(eventId);

            return markers.Select(m => new MarkerDto
            {
                Id = m.Id,
                Name = m.Name,
                Type = m.Type,
                SpaceId = m.SpaceId
            });
        }

        public async Task<bool> UpdateAsync(MarkerDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var existingMarker = await _markerRepository.GetByIdAsync(dto.Id);
            if (existingMarker == null)
                throw new ArgumentException($"Marker with Id={dto.Id} was not found.");

            existingMarker.Name = dto.Name;
            existingMarker.Type = dto.Type;
            existingMarker.SpaceId = dto.SpaceId;

            return await _markerRepository.UpdateAsync(existingMarker);
        }

        public async Task<bool> AddMarkerToEventAsync(long eventId, long markerId)
        {
            return await _markerRepository.AddMarkerToEventAsync(eventId, markerId);
        }

        public async Task<bool> RemoveMarkerFromEventAsync(long eventId, long markerId)
        {
            return await _markerRepository.RemoveMarkerFromEventAsync(eventId, markerId);
        }

    }
}
