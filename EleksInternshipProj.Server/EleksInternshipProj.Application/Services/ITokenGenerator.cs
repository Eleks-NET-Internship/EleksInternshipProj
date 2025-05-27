using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services
{
    public interface ITokenGenerator
    {
        public string GenerateToken(long userId, string email);
    }
}
