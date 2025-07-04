﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Infrastructure.Extensions;

namespace EleksInternshipProj.Infrastructure.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly string _secret;

        private readonly string _issuer;

        private readonly string _audience;

        public TokenGenerator(IConfiguration configuration)
        {
            _secret = configuration.GetRequiredConfig("Jwt", "Secret");
            _issuer = configuration.GetRequiredConfig("Jwt", "Issuer");
            _audience = configuration.GetRequiredConfig("Jwt", "Audience");
        }

        public string GenerateToken(long userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);

            List<Claim> claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.Email, email.ToString())
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
