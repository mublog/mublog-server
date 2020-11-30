using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mublog.Server.Infrastructure.Common.Config;
using Serilog;

namespace Mublog.Server.PublicApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DbConnectionStringBuilder.Build();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = context.HostingEnvironment;
                    builder.AddYamlFile("appsettings.yml", optional: false)
                        .AddYamlFile($"appsettings.{env.EnvironmentName}.yml", optional: true);
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}