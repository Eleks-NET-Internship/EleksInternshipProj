using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;
        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA3_256;
        public (byte[] hash, byte[] salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithm);
            byte[] hashBytes = pbkdf2.GetBytes(HashSize);

            return (hashBytes, saltBytes);
        }

        public bool IsPasswordCorrect(byte[] storedHash, byte[] storedSalt, string providedPassword)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, storedSalt, Iterations, HashAlgorithm);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            bool isMatch = CryptographicOperations.FixedTimeEquals(hash, storedHash);
            return isMatch;
        }
    }
}
