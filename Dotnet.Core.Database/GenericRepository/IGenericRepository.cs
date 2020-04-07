using Dotnet.Core.Todos.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donet.Core.Todos.Data.GenericRepository
{
    public interface IGenericRepository<TEntity>
    where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllList();

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        Task<int> CreateAsync(TEntity entity);
        int Create(TEntity entity);

        Task UpdateAsync(TEntity entity);
        void Update(TEntity entity);

        Task DeleteAsync(int id);
        void Delete(int id);
    }

}
