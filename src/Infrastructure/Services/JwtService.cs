using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Mublog.Server.Infrastructure.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Mublog.Server.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly ISecurityKeyService _keyService;

        public JwtService(ISecurityKeyService keyService)
        {
            _keyService = keyService;
        }
        
        public JwtSecurityToken GetToken(string subClaim)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subClaim)
            };

            var key = _keyService.GetKey();
            var algorithm = _keyService.SecurityAlgorithm;
            
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(7),
                signingCredentials
            );

            return token;
        }

        public string GetTokenString(string subClaim)
        {
            var token = GetToken(subClaim);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}