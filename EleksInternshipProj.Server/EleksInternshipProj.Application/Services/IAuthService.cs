using System.Security.Claims;
using Task = System.Threading.Tasks.Task;

using EleksInternshipProj.Application.DTOs;

namespace EleksInternshipProj.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);

        Task<string> LoginAsync(LoginRequest request);

        Task<string> GoogleLoginAsync(ClaimsPrincipal claimsPrincipal);
    }
}
