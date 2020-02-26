using Dotnet.Core.Todos.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Dotnet.Core.Todos.Api.Configurations
{
    public static class HateoasLinks
    {
        public static void ConfigureLinks(this IMvcBuilder services)
        {
            services.AddHateoas(options =>
            {
                options
                   .AddLink<TodoViewModel>("get-todo", p => new { id = p.Id })
                   .AddLink<IEnumerable<TodoViewModel>>("all")
                   .AddLink<TodoViewModel>("update-todo", p => p)
                   .AddLink<TodoViewModel>("create-todo", p => p)
                   .AddLink<TodoViewModel>("delete-todo", p => new { id = p.Id });
            });
        }
    }
}
