
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace EleksInternshipProj.Infrastructure.Repositories
{
    public class MarkerRepository : IMarkerRepository
    {
        private readonly NavchaykoDbContext _context;

        public MarkerRepository(NavchaykoDbContext context)
        {
            _context = context;
        }

        public async Task<Marker?> GetByIdAsync(long id)
        {
            return await _context.Markers.FindAsync(id);
        }

        public async Task<IEnumerable<Marker>> GetAllAsync()
        {
            return await _context.Markers.ToListAsync();
        }

        public async Task<IEnumerable<Marker>> GetAllByEventIdAsync(long eventId)
        {
            return await _context.EventMarkers
                .Where(em => em.EventId == eventId)
                .Include(em => em.Marker)
                .Select(em => em.Marker)
                .ToListAsync();
        }

        public async Task<Marker> AddAsync(Marker marker)
        {
            _context.Markers.Add(marker);
            await _context.SaveChangesAsync();
            return marker;
        }

        public async Task<bool> UpdateAsync(Marker marker)
        {
            _context.Markers.Update(marker);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var marker = await _context.Markers.FindAsync(id);
            if (marker == null)
                return false;

            _context.Markers.Remove(marker);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> AddMarkerToEventAsync(long eventId, long markerId)
        {
            // Check if this note already exist
            var exists = await _context.EventMarkers.AnyAsync(em => em.EventId == eventId && em.MarkerId == markerId);
            if (exists)
                return false;

            var eventMarker = new EventMarker
            {
                EventId = eventId,
                MarkerId = markerId
            };

            _context.EventMarkers.Add(eventMarker);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> RemoveMarkerFromEventAsync(long eventId, long markerId)
        {
            var eventMarker = await _context.EventMarkers
                .FirstOrDefaultAsync(em => em.EventId == eventId && em.MarkerId == markerId);

            if (eventMarker == null)
                return false;

            _context.EventMarkers.Remove(eventMarker);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }
    }
}
