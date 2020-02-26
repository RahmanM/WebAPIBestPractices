using Donet.Core.Todos.Data.GenericRepository;
using Dotnet.Core.Todos.Database;

namespace Donet.Core.Todos.Data.Repositories.TodoRepository
{
    public interface ITodoRepository : IGenericRepository<Todo>
    {

    }

    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoContext dbContext) : base(dbContext)
        {
        }

        
    }
}
