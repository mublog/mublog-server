using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mublog.Server.PublicApi.Config
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _versionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider versionProvider)
        {
            _versionProvider = versionProvider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _versionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new OpenApiInfo
                {
                    Title = $"µblog API V{desc.ApiVersion}",
                    Version = desc.ApiVersion.ToString(),
                    Description = "The backend of µblog.",
                    License = new OpenApiLicense
                        {Name = "MIT", Url = new Uri("https://github.com/mublog/mublog-server/blob/master/LICENSE")}
                });

                var xmLCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmLCommentFile);
                options.IncludeXmlComments(cmlCommentFullPath);
            }
        }
    }
}