using System.Text;
using Microsoft.IdentityModel.Tokens;
using Mublog.Server.Infrastructure.Services.Interfaces;

namespace Mublog.Server.Infrastructure.Services
{
    public class SymmetricKeyService : ISecurityKeyService
    {
        public string SecurityAlgorithm => _algorithm;

        private string _algorithm = SecurityAlgorithms.HmacSha256; // TODO implement config via app settings
        
        public SecurityKey GetKey()
        {
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret); // TODO implement config via app settings
            return new SymmetricSecurityKey(secretBytes);
        }
    }
}