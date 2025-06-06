using EleksInternshipProj.Application.DTOs;

namespace EleksInternshipProj.Application.Services
{
    public interface IProfileService
    {
        Task<ProfileDto> GetViewInfoAsync(long userId);

        Task UpdateProfile(long userId, UpdateProfileDto profileDto);
    }
}
