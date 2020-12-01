using System.IdentityModel.Tokens.Jwt;

namespace Mublog.Server.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        JwtSecurityToken GetToken(string subject);
        string GetTokenString(string subject);
    }
}