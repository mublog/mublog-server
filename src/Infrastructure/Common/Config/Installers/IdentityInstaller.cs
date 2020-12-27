using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.Infrastructure.Identity;
using Mublog.Server.Infrastructure.Services;
using Mublog.Server.Infrastructure.Services.Interfaces;

namespace Mublog.Server.Infrastructure.Common.Config.Installers
{
    public class IdentityInstaller : IInstaller
    {
        
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<ICurrentUserService, JwtSubExtractorService>();
        }
       
    }
}