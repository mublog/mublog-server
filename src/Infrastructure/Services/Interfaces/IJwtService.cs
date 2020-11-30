using System.IdentityModel.Tokens.Jwt;

namespace Mublog.Server.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        JwtSecurityToken GetToken(string subClaim);
        string GetTokenString(string subClaim);
    }
}