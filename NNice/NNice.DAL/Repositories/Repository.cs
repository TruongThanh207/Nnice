using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNice.DAL.Repositories
{
    public class Repository<TContext> : IRepository where TContext : DbContext
    {

        private readonly TContext _context;
        public Repository(TContext context)
        {
            _context = context;

        }

        public async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> CreateReturnAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (skip.HasValue)
                query = query.Take(take.Value);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return entity;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
        }
        public async Task ExecuteAsync(string command, params object[] @params)
        {
            await _context.Database.ExecuteSqlCommandAsync(command, @params);
        }
    }
}
