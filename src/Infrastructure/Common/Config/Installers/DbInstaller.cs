using System.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProfiler.Integrations;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper;
using Mublog.Server.Infrastructure.Data.Repositories;
using Npgsql;
using StackExchange.Profiling.Data;


namespace Mublog.Server.Infrastructure.Common.Config.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            Configure();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // var cp = new CustomDbProfiler();
            // services.AddSingleton<IDbProfiler>(cp);
            // services.AddTransient<IDbConnection>((sp) => new ProfiledDbConnection(new NpgsqlConnection(connectionString), cp));
            
            services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(connectionString));
            
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            services.AddAutoMapper(typeof(Mappings.Automapper.Mappings));
        }

        private void Configure()
        {
            DapperMapper.Initialize();
        }
    }
}