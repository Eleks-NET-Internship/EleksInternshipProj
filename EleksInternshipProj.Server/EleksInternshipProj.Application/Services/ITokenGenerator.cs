namespace EleksInternshipProj.Application.Services
{
    public interface ITokenGenerator
    {
        public string GenerateToken(long userId, string email);
    }
}
