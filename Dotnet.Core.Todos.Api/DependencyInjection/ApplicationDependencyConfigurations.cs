using Donet.Core.Todos.Data.Repositories.TodoRepository;
using Dotnet.Core.Todos.Data;
using Dotnet.Core.Todos.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Core.Todos.Api.DependencyInjection
{
    /// <summary>
    /// Application entities DI configurations
    /// </summary>
    public static class ApplicationDependencyConfigurations
    {
        /// <summary>
        /// Configure Application Dependencies
        /// </summary>
        public static void ConfigureApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDtoQuery<TodoDto>, AllTodosQuery>();
            services.AddScoped<IDtoQuery<TodoByCategory>, TodosGroupByCategoryQuery>();
            services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}
