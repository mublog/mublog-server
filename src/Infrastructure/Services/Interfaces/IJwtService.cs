using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mublog.Server.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        JwtSecurityToken GetToken(string username, long profileId, long accountId, string email = "",  IEnumerable<Claim> additionalClaims = null);
        string GetTokenString(string username, long profileId, long accountId, string email = "",  IEnumerable<Claim> additionalClaims = null);
    }
}