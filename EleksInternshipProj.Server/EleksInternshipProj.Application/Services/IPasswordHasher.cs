using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services
{
    public interface IPasswordHasher
    {
        public (byte[] hash, byte[] salt) HashPassword(string password);
        public bool IsPasswordCorrect(byte[] storedHash, byte[] storedSalt, string providedPassword);
    }
}
