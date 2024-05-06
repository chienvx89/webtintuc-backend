using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Website.Infrastructure.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        int Count(Expression<Func<TEntity, bool>> filter = null);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool enableTracking = true, int offset = 0, int limit = -1);

        IEnumerable<TEntity> Get(out int count, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool enableTracking = true, int offset = 0, int limit = -1);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                        bool enableTracking = true);

        Task Insert(TEntity entity);
        Task InsertRange(IEnumerable<TEntity> entities);
        Task Delete(object id);
        Task Delete(TEntity entityToDelete);
        Task DeleteRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entityToUpdate);
        Task UpdateRange(IEnumerable<TEntity> entitiesToUpdate);

        IEnumerable<TEntity> GetDataFromSqlRaw(string sqlQuery, bool enableTracking = false);
    }
}
