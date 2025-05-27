using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;
using Task = System.Threading.Tasks.Task;
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

        public async Task RegisterAsync(RegisterRequest request)
        {
            //mock registration, email should be verified by sending and checking confirmation code

            User? existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }
            (byte[] hash, byte[] salt) = _passwordHasher.HashPassword(request.Password);

            User newUser = new User
            { //UserName should be username, it's one word
                Email = request.Email,
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = hash,
                PasswordSalt = salt,
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
    }
}
