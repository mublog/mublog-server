using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.Infrastructure.Services.Interfaces;

namespace Mublog.Server.Infrastructure.Services.Installers
{
    public class JwtInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISecurityKeyService, SymmetricKeyService>();
            services.AddSingleton<IJwtService, JwtService>();
        }
    }
}