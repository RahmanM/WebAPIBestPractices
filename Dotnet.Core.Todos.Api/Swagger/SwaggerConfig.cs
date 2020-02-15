using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dotnet.Core.Todos.Api.Swagger
{
    /// <summary>
    /// #SwaggerDocs
    /// </summary>
    public class SwaggerConfig
    {
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "v1 API",
                        Description = "v1 API Description",
                        TermsOfService = new Uri("https://rahmanmahmoodi.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Rahman Mahmoodi",
                            Email = string.Empty,
                            Url = new Uri("https://twitter.com/rahmanmahmoodi"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = new Uri("https://rahmanmahmoodi.com/license"),
                        }
                    });

                // Add a SwaggerDoc for v2 
                options.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Version = "v2",
                        Title = "v2 API",
                        Description = "v2 API Description",
                        TermsOfService = new Uri("https://rahmanmahmoodi.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Rahman Mahmoodi",
                            Email = string.Empty,
                            Url = new Uri("https://twitter.com/rahmanmahmoodi.com"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = new Uri("https://rahmanmahmoodi.com/license"),
                        }
                    });

                // These are to enable the version number in the Version mandatory field when clicking the API end point
                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // This is to enable to filter the API when selecting the API version in dropdown and populate only related version end points
                options.DocInclusionPredicate((version, desc) =>
                {

                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);


                    var maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == version)
                           && (!maps.Any() || maps.Any(v => $"v{v.ToString()}" == version));
                });

                // This is to get model and end point comments from the code and generate XML file for more docs
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            });

        }

        /// <summary>
        /// This is to configure the swagger UI versions dropdown box to show different versions
        /// </summary>
        public static void ConfigureApp(IApplicationBuilder app)
        {
            // And add this, an endpoint for our swagger doc 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
                c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
            });
        }
    }


}
