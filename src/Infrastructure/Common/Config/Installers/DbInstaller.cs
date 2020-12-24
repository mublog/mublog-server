using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.Infrastructure.Data.Mappings.Dapper;


namespace Mublog.Server.Infrastructure.Common.Config.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Mappings));
            
            DapperMapper.Activate();
        }
    }
}