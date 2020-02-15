using Dotnet.Core.Todos.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Core.Todos.Data
{

    public class AllTodosQuery : IDtoQuery<TodoDto>
    {
        private TodoContext _Context;

        public AllTodosQuery(TodoContext context)
        {
            _Context = context;
        }

        public IEnumerable<TodoDto> Execute()
        {
            return ExecuteAsync().Result;
        }

        public async Task<IEnumerable<TodoDto>> ExecuteAsync()
        {
            var todos = await _Context.Todos.OrderByDescending(o => o.Id).ToListAsync();
            var todosDto = new List<TodoDto>();

            todos.ForEach(t =>
            {
                todosDto.Add(new TodoDto
                {
                    Active = t.Active,
                    CategoryId = t.CategoryId,
                    Completed = t.Completed,
                    Description = t.Description,
                    Id = t.Id,
                    Category = t.Category?.Description
                });
            });

            return todosDto;
        }
    }
}
