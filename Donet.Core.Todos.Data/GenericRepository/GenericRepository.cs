using Dotnet.Core.Todos.Common.ExceptionTypes;
using Dotnet.Core.Todos.Data;
using Dotnet.Core.Todos.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public virtual int Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            // TODO: Apply model validation to make sure all properties are provided - Throw Business Exception

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public virtual void Delete(int id)
        {
            if (id <= 0)
            {
                throw new BusinessException("Id is required!");
            }

            var entity = GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new BusinessException("Id is required!");
            }

            var entity = await GetByIdAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAllList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException("Id is required!");
            }

            return _dbContext.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException("Id is required!");
            }

            return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity.Id <= 0)
            {
                throw new BusinessException("Entity Id should be greater than zero!");
            }

            var entry = _dbContext.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            // TODO: Apply model validation to make sure all properties are provided - Throw Business Exception

            if (entity.Id <= 0)
            {
                throw new BusinessException("Entity Id should be greater than zero!");
            }

            var entry = _dbContext.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }

}
