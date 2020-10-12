using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace NNice.DAL.Repositories
{
    public interface IRepository
    {

        Task AddAsync<T>(T entity)
            where T : class;

        Task<T> CreateReturnAsync<T>(T entity)
            where T : class;

        void Delete<T>(T entity)
            where T : class;

        void Update<T>(T entity)
            where T : class;

        Task<T> GetByIdAsync<T>(object id)
            where T : class;

        Task SaveAsync();

        Task<T> GetOneAsync<T>(Expression<Func<T,bool>> expression)
            where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null)
            where T : class;
        Task ExecuteAsync(string command, params object[] @params);

    }
}
