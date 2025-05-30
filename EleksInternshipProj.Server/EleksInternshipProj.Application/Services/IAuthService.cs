using System.Security.Claims;
using Task = System.Threading.Tasks.Task;

using EleksInternshipProj.Application.DTOs;

namespace EleksInternshipProj.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);

        Task<string> ValidateUser(LoginRequest request);

        Task<string> GoogleLogin(ClaimsPrincipal claimsPrincipal);
    }
}
