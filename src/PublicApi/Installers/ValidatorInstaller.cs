using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Infrastructure.Validators;
using Mublog.Server.Infrastructure.Validators.Interfaces;

namespace Mublog.Server.PublicApi.Installers
{
    public class ValidatorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailValidator, SimpleEmailValidator>();
        }
    }
}