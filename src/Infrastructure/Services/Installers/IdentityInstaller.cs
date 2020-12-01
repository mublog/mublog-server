using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.Infrastructure.Data;
using Mublog.Server.Infrastructure.Identity;

namespace Mublog.Server.Infrastructure.Services.Installers
{
    public class IdentityInstaller : IInstaller
    {
        
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
       
    }
}