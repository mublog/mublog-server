using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Mublog.Server.Infrastructure.Common.Config;

namespace Mublog.Server.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(Directory.GetCurrentDirectory() + "/../PublicApi/appsettings.Development.json")
                    .Build();
            else
                configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(Directory.GetCurrentDirectory() + "/../PublicApi/appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(DbConnectionStringBuilder.Build());
            return new AppDbContext(builder.Options);
        }
    }
}