using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.PublicApi.Validators;
using Mublog.Server.PublicApi.Validators.Interfaces;

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