using System.Security.Claims;
using Task = System.Threading.Tasks.Task;

using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        // Local auth
        public async Task RegisterAsync(RegisterRequest request)
        {
            // Mock registration, email should be verified by sending and checking confirmation code
            User? existingUser = await _userRepository.GetByEmailAndProviderAsync(request.Email, "local");
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            request.Email = request.Email.Trim();
            request.Username = request.Username.Trim();
            request.FirstName = request.FirstName?.Trim();
            request.LastName = request.LastName?.Trim();

            if (request.Username.Length < 1)
            {
                throw new Exception("Username can't be empty");
            }
            else if (request.Email.Length < 3)
            {
                throw new Exception("Invalid email");
            }
            else if (request.Password.Length < 1)
            { // Add password validation
                throw new Exception("Invalid password");
            }

            (byte[] hash, byte[] salt) = _passwordHasher.HashPassword(request.Password);
            User newUser = new User
            {
                Username = request.Username,
                FirstName = request.FirstName == "" ? null : request.FirstName,
                LastName = request.LastName == "" ? null : request.FirstName,
                Email = request.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                AuthProvider = "local",
                ExternalId = null
            };

            await _userRepository.AddUserAsync(newUser);
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            User? existingUser = await _userRepository.GetByEmailAndProviderAsync(request.Email, "local");
            if (existingUser == null ||
                !_passwordHasher.IsPasswordCorrect(existingUser.PasswordHash, existingUser.PasswordSalt, request.Password))
            {
                throw new Exception("Invalid credentials");
            }

            return _tokenGenerator.GenerateToken(existingUser.Id, existingUser.Email);
        }

        //Google auth
        public async Task<string> GoogleLoginAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                throw new Exception("Invalid email");
            }

            User? existingUser = await _userRepository.GetByEmailAndProviderAsync(email, "google");
            if (existingUser != null)
            {
                return _tokenGenerator.GenerateToken(existingUser.Id, email);
            }
            
            long userId = await GoogleRegisterAsync(claimsPrincipal);

            return _tokenGenerator.GenerateToken(userId, email);
        }

        private async Task<long> GoogleRegisterAsync(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            User user = new User
            {
                Username = email.Substring(0, email.IndexOf('@')),
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName),
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname),
                Email = email,
                PasswordHash = null,
                PasswordSalt = null,
                AuthProvider = "google",
                ExternalId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _userRepository.AddUserAsync(user);

            return await _userRepository.GetIdByEmailAndProviderAsync(email, "google");
        }
    }
}
