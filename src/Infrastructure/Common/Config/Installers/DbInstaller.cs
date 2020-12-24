using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;


namespace Mublog.Server.Infrastructure.Common.Config.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(Mappings));
        }
    }
}