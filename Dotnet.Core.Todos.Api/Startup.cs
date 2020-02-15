using Dotnet.Core.Todos.Api.Action_Filters;
using Dotnet.Core.Todos.Api.ExceptionHandler;
using Dotnet.Core.Todos.Api.Swagger;
using Dotnet.Core.Todos.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dotnet.Core.Todos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // #ContentNegociation
            services.AddMvc(config =>
            {
                // Add XML Content Negotiation
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter(null));
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });

            // #ApiVersioning
            services.AddApiVersioning();

            // #Swagger
            SwaggerConfig.ConfigureService(services);

            services.AddControllers();

            services.AddScoped<ModelValidationAttribute>();

            // #injectingcontext
            services.AddDbContext<TodoContext>(options => options.UseMySql("server = localhost; user id = todoUser; password = todoUser; port = 3306; database = todos;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // #Swagger
            SwaggerConfig.ConfigureApp(app);

            // # globalexceptionhandler
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
