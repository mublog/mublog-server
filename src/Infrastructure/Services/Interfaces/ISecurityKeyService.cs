using Microsoft.IdentityModel.Tokens;

namespace Mublog.Server.Infrastructure.Services.Interfaces
{
    public interface ISecurityKeyService
    {
        SecurityKey GetKey();
    }
}