using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.PublicApi.Config;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mublog.Server.PublicApi.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen();
        }
    }
}