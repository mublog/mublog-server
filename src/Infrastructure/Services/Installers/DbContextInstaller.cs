using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mublog.Server.Application.Common.Interfaces;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Common.Config;
using Mublog.Server.Infrastructure.Data;
using Mublog.Server.Infrastructure.Data.Repositories;

namespace Mublog.Server.Infrastructure.Services.Installers
{
    public class DbContextInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("defaultConnection")));

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostImageRepository, PostImageRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileImageRepository, ProfileImageRepository>();

            services.AddAutoMapper(typeof(Mappings));
        }
    }
}