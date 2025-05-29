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
            User? existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            request.Email = request.Email.Trim();
            request.Username = request.Username.Trim();
            request.FirstName = request.FirstName.Trim();
            request.LastName = request.LastName.Trim();


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

        public async Task<string> ValidateUser(LoginRequest request)
        {
            User? existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser == null ||
                !_passwordHasher.IsPasswordCorrect(existingUser.PasswordHash, existingUser.PasswordSalt, request.Password))
            {
                throw new Exception("Invalid credentials");
            }

            return _tokenGenerator.GenerateToken(existingUser.Id, existingUser.Email);
        }

        //Google auth
    }
}
