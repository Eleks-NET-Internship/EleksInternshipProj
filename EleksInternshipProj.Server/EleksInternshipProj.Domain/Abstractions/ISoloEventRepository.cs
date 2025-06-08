using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleksInternshipProj.Domain.Models;


namespace EleksInternshipProj.Domain.Abstractions
{
    public interface ISoloEventRepository
    {
        Task<IEnumerable<SoloEvent>> GetAllBySpaceIdAsync(long spaceId);
        Task<SoloEvent?> GetByIdAsync(long id);
        Task<SoloEvent?> AddAsync(SoloEvent entity);
        Task<bool> UpdateAsync(SoloEvent entity);
        Task<bool> DeleteAsync(long id);
    }

}
