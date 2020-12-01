using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.Infrastructure.Data;
using Mublog.Server.Infrastructure.Identity;
using Mublog.Server.Infrastructure.Services.Interfaces;

namespace Mublog.Server.Infrastructure.Services.Installers
{
    public class IdentityInstaller : IInstaller
    {
        private SecurityKey _securityKey;
        
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            TokenSetup();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var publicUrl = configuration.GetSection("publicUrl").Value;
                    
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = publicUrl,
                        ValidAudience = publicUrl,
                        IssuerSigningKey = _securityKey
                    };
                });

            services.AddSingleton<SecurityKey>(_securityKey);
            services.AddSingleton<IJwtService, JwtService>();
        }
        private void TokenSetup()
        {
            if (File.Exists("../data/secrets/jwtsignkey"))   // TODO consider storage location
            {
                LoadSecret();
            }
            else if (!Directory.Exists("../data"))
            {
                Directory.CreateDirectory("../data");
                Directory.CreateDirectory("../data/secrets");
                GenSecret();
            }
            else if (!Directory.Exists("../data/secrets"))
            {
                Directory.CreateDirectory("../data/secrets");
                GenSecret();
            }
            else
            {
                GenSecret();
            }
        }

        private void GenSecret()
        {
            var secretBytes = new byte[256];

            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(secretBytes);
            var secretString = Convert.ToBase64String(secretBytes);

            File.WriteAllText("../data/secrets/jwtsignkey", secretString, Encoding.UTF8);

            _securityKey = new SymmetricSecurityKey(secretBytes);
        }

        private void LoadSecret()
        {
            var secretString = File.ReadAllText("../data/secrets/jwtsignkey", Encoding.UTF8);
            var secretBytes = Convert.FromBase64String(secretString);
            _securityKey = new SymmetricSecurityKey(secretBytes);
        }
    }
}