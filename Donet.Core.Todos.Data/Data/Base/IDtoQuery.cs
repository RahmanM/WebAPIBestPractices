using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotnet.Core.Todos.Data
{
    public interface IDtoQuery<T> where T : IEntity
    {
        public IEnumerable<T> Execute();

        public Task<IEnumerable<T>> ExecuteAsync();
    }
}
