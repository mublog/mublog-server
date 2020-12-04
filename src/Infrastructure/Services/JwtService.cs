using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        
        public JwtSecurityToken GetToken(string username, string email = "",  IEnumerable<Claim> additionalClaims = null)
        {
            var claims = new List<Claim>();
            
            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }
            
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = _securityKey;
            const string algorithm = SecurityAlgorithms.HmacSha256;
            
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

        public string GetTokenString(string username, string email = "",  IEnumerable<Claim> additionalClaims = null)
        {
            var token = GetToken(username, email, additionalClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}