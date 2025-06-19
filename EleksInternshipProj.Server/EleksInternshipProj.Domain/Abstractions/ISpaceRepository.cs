using EleksInternshipProj.Domain.Models;
using System.Dynamic;

namespace EleksInternshipProj.Domain.Abstractions
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetByUserAsync(long userId);
        Task<Space?> GetByIdAsync(long id);
        Task<Space?> AddAsync(Space space, long userId);
        Task<Space?> AddToAsync(long spaceId, long userId, long roleId);
        Task<Space?> UpdateAsync(Space space);
        Task<bool> DeleteAsync(long id);
    }
}
