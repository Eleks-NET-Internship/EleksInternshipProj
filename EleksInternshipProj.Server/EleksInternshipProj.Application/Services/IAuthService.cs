using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace EleksInternshipProj.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task<string> ValidateUser(LoginRequest request);
    }
}
