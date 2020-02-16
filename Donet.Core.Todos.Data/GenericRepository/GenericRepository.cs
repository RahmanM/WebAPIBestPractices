using Dotnet.Core.Todos.Data;
using Dotnet.Core.Todos.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donet.Core.Todos.Data.GenericRepository
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntity
    {
        private readonly TodoContext _dbContext;

        public GenericRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(TEntity entity)
        {
            // TODO: Apply model validation to make sure all properties are provided - Throw Business Exception

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException("Id is required!");
            }

            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException("Id is required!");
            }

            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(int id, TEntity entity)
        {
            // TODO: Apply model validation to make sure all properties are provided - Throw Business Exception

            if (entity.Id <= 0)
            {
                throw new InvalidOperationException("Entity Id should be greater than zero!");
            }

            var entry = _dbContext.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }

}
