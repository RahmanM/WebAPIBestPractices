using AutoMapper;
using Dotnet.Core.Todos.Api.Models;
using Dotnet.Core.Todos.Data;
using Dotnet.Core.Todos.Database;

namespace Dotnet.Core.Todos.Api.Controllers
{
    /// <summary>
    /// Automapper that maps TodoDto to TodoViewModel
    /// This is called in application startup
    /// </summary>
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Database.Todo, TodoViewModel>();
            CreateMap<TodoViewModel, Database.Todo>();
        }
    }
}
