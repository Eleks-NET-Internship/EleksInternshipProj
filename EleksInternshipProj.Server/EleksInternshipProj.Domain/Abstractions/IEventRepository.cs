using EleksInternshipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Domain.Abstractions
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(long id);
        Task<Event?> AddAsync(Event entity);
        Task<bool> UpdateAsync(Event entity);
        Task<bool> DeleteAsync(long id);
    }

}
