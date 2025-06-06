using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class ProfileService : IProfileService
    {
        public IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ProfileDto> GetViewInfoAsync(long userId)
        {
            User? user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            ProfileDto dto = new ProfileDto
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return dto;
        }
    }
}
