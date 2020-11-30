using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;

namespace Mublog.Server.Infrastructure.Services.Installers
{
    public static class InfrastructureInstaller
    {
        public static void InstallInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var installers =
                Assembly.GetExecutingAssembly()
                    .ExportedTypes.Where(x => typeof(IInstaller)
                        .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}