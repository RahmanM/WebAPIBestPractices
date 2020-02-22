using AutoMapper;
using Dotnet.Core.Todos.Api.Action_Filters;
using Dotnet.Core.Todos.Api.DependencyInjection;
using Dotnet.Core.Todos.Api.ExceptionHandler;
using Dotnet.Core.Todos.Api.Swagger;
using Dotnet.Core.Todos.Database;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sieve.Services;

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

                // #GlobalFilters
                config.Filters.Add(typeof(ModelValidationAttribute));
            }).AddFluentValidation(options =>
            {
                // # FluentValidaton
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            }); 


            // #AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // #ApiVersioning
            services.AddApiVersioning();

            // Register lazy cache
            services.AddLazyCache();

            // #Swagger
            SwaggerConfig.ConfigureService(services);

            // Add sorting, filtering, paging 
            services.AddScoped<SieveProcessor>();

            services.AddControllers();

            services.AddScoped<ModelValidationAttribute>();

            // #injectingcontext
            services.AddDbContext<TodoContext>(options => options.UseMySql("server = localhost; user id = todoUser; password = todoUser; port = 3306; database = todos;"));

            // Application level services DI configuration
            services.ConfigureApplicationDependencies();
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
