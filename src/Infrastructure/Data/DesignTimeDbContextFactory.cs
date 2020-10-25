using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mublog.Server.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration;
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Mublog.Api/appsettings.Development.json").Build();
            else
                configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Mublog.Api/appsettings.json").Build();
            
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            var connectionString = configuration.GetConnectionString(DbConnectionStringBuilder.Build());
            builder.UseNpgsql(connectionString);
            return new IdentityContext(builder.Options);
        }
    }
}