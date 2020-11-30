using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Mublog.Server.Infrastructure.Services.Interfaces;

namespace Mublog.Server.Infrastructure.Services
{
    public class SymmetricKeyService : ISecurityKeyService
    {
        private readonly ILogger<SymmetricKeyService> _logger;
        private bool _initialized = false;
        private SecurityKey _securityKey;

        public SymmetricKeyService(ILogger<SymmetricKeyService> logger)
        {
            _logger = logger;
            Initialize();
        }
        
        public SecurityKey GetKey()
        {
            if (!_initialized) Initialize();
            return _securityKey;
        }

        private void Initialize()
        {
            if (File.Exists("./data/secrets/jwtsignkey"))
            {
                LoadSecret();
            }
            else if (!Directory.Exists("./data"))
            {
                Directory.CreateDirectory("./data");
                Directory.CreateDirectory("./data/secrets");
                GenSecret();
            }
            else if (!Directory.Exists("./data/secrets"))
            {
                Directory.CreateDirectory("./data/secrets");
                GenSecret();
            }
            else
            {
                GenSecret();
            }

            _initialized = true;
        }

        private void GenSecret()
        {
            var secretBytes = new byte[256];
            
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(secretBytes);
            
            var secretString = Encoding.UTF8.GetString(secretBytes);
            _logger.LogWarning(secretString);

            File.WriteAllText("./data/secrets/jwtsignkey", secretString, Encoding.UTF8);
            
            _securityKey = new SymmetricSecurityKey(secretBytes);
        }

        private void LoadSecret()
        {
            var secretString = File.ReadAllText("./data/secrets/jwtsignkey");
            _logger.LogWarning(secretString);
            var secretBytes = Encoding.UTF8.GetBytes(secretString);
            _securityKey = new SymmetricSecurityKey(secretBytes);
        }
    }
}