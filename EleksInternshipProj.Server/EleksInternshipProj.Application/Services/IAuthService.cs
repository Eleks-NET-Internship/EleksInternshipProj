using EleksInternshipProj.Application.DTOs;
using EleksInternsipProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task ValidateUser(LoginRequest request);
    }
}
