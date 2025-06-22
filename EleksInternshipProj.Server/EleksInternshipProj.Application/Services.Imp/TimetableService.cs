using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Application.Mappers;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableRepository _timetableRepository;

        public TimetableService(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async Task<Timetable?> GetBySpaceAsync(long spaceId)
        {
            return await _timetableRepository.GetBySpaceAsync(spaceId);
        }

        public async Task<Timetable?> UpdateAsync(TimetableDto dto)
        {
            var updatedTimetable = dto.ToEntity();
            var existingTimetable = await _timetableRepository.GetBySpaceAsync(dto.SpaceId);
            if (existingTimetable == null)
            {
                return null; // Timetable not found for the given space
            }

            // Update the existing timetable with new values
            existingTimetable.Days = updatedTimetable.Days;
            existingTimetable.SpaceId = updatedTimetable.SpaceId;
            
            return await _timetableRepository.UpdateAsync(existingTimetable);
        }
    }
}