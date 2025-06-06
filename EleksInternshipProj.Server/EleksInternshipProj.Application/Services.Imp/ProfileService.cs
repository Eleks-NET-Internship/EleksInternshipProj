using Task = System.Threading.Tasks.Task;

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

        public async Task UpdateProfile(long userId, UpdateProfileDto profileDto)
        {
            User? user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }


            profileDto.Username = profileDto.Username.Trim();
            profileDto.FirstName = profileDto.FirstName?.Trim();
            profileDto.LastName = profileDto.LastName?.Trim();

            if (profileDto.Username.Length < 1)
            {
                throw new Exception("Username can't be empty");
            }

            user.Username = profileDto.Username;
            user.FirstName = profileDto.FirstName == "" ? null : profileDto.FirstName;
            user.LastName = profileDto.LastName == "" ? null : profileDto.LastName;

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
