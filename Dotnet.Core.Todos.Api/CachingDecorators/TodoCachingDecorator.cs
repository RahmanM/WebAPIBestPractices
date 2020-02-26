using Donet.Core.Todos.Data.Repositories.TodoRepository;
using Dotnet.Core.Todos.Database;
using LazyCache;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Core.Todos.Api.CachingDecorators
{
    /// <summary>
    /// Caching Decorator that returns Todos from cache until a todo is added, updated or removed!
    /// </summary>
    public class TodoCachingDecorator : TodoRepository
    {
        private const string CACHE_KEY = "Todo.GetAll";
        private readonly IAppCache _cache;

        public TodoCachingDecorator(TodoContext context, IAppCache cache) : base(context)
        {
            _cache = cache;
        }

        public override async Task<int> CreateAsync(Todo entity)
        {
            var id = await base.CreateAsync(entity);
            if (id > 0)
            {
                _cache.Remove(CACHE_KEY);
            }
            return id;
        }

        public override int Create(Todo entity)
        {
            var id = base.Create(entity);
            if (id > 0)
            {
                _cache.Remove(CACHE_KEY);
            }
            return id;
        }

        public override async Task UpdateAsync(Todo entity)
        {
            await base.UpdateAsync(entity);
            _cache.Remove(CACHE_KEY);
        }

        public override void Update(Todo entity)
        {
            base.Update(entity); 
            _cache.Remove(CACHE_KEY);
        }

        public override IEnumerable<Todo> GetAllList()
        {
            return _cache.GetOrAdd(CACHE_KEY, () => base.GetAllList());
        }

        public override async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
            _cache.Remove(CACHE_KEY);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
            _cache.Remove(CACHE_KEY);
        }

    }
}
