namespace EleksInternshipProj.Application.Services
{
    public interface IPasswordHasher
    {
        public (byte[] hash, byte[] salt) HashPassword(string password);

        public bool IsPasswordCorrect(byte[] storedHash, byte[] storedSalt, string providedPassword);
    }
}
