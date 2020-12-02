using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;

namespace Mublog.Server.PublicApi.Installers
{
    public static class ApiInstaller
    {
        public static void InstallApi(this IServiceCollection services, IConfiguration configuration)
        {
            var installers =
                typeof(Startup).Assembly
                    .ExportedTypes.Where(x => typeof(IInstaller)
                        .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}