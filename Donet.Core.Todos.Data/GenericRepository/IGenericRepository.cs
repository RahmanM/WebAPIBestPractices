using Dotnet.Core.Todos.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Donet.Core.Todos.Data.GenericRepository
{
    public interface IGenericRepository<TEntity>
    where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task<int> Create(TEntity entity);

        Task Update(int id, TEntity entity);

        Task Delete(int id);
    }

}
