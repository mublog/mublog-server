using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mublog.Server.Infrastructure.Common.Config.Installers;
using Mublog.Server.Infrastructure.Common.Helpers;
using Mublog.Server.PublicApi.Common.Config.Installers;
using Serilog;

namespace Mublog.Server.PublicApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureLogger();
            ConfigureDbConnection();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallInfrastructure(Configuration);
            services.InstallApi(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider versionProvider)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in versionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
                    options.RoutePrefix = "swagger";
                }
            });

            // app.UseHttpsRedirection(); // Managed by NGINX

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            // TODO serve files from /media Under url /media 

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        private void ConfigureDbConnection()
        {
            var connectionString = DbConnectionStringBuilder.Build();
            
            if (connectionString != null)
            {
                Configuration.GetSection("connectionStrings").GetSection("defaultConnection").Value = connectionString;
            }
        }
    }
}