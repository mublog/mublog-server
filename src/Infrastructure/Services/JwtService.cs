using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mublog.Server.Infrastructure.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Mublog.Server.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly SecurityKey _securityKey;
        private readonly IConfiguration _configuration;

        public JwtService(SecurityKey securityKey, IConfiguration configuration)
        {
            _securityKey = securityKey;
            _configuration = configuration;
        }
        
        public JwtSecurityToken GetToken(string subject)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject)
            };

            var key = _securityKey;
            var algorithm = SecurityAlgorithms.HmacSha256;
            
            var signingCredentials = new SigningCredentials(key, algorithm);

            var now = DateTime.UtcNow;
            var publicUrl = _configuration.GetSection("publicUrl").Value;

            var token = new JwtSecurityToken(
                publicUrl,
                publicUrl,
                claims,
                notBefore: now,
                expires: now.AddDays(7),
                signingCredentials
            );

            return token;
        }

        public string GetTokenString(string subject)
        {
            var token = GetToken(subject);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}