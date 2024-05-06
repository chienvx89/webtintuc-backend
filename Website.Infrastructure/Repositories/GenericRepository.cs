using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Infrastructure.IRepositories;
using Website.Infrastructure.Contexts;

namespace Website.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal WebContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(WebContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet; query = query.AsNoTracking();
            if (filter != null)
                query = query.Where(filter);
            return query.Count();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool enableTracking = true, int offset = 0, int limit = -1)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (include != null) query = include(query);

            if (orderBy != null)
                return orderBy(query);

            //limit = -1: get all data
            if (limit != -1)
            {
                return query.Skip(offset).Take(limit);
            }
            return query.AsSplitQuery();
        }

        public virtual IEnumerable<TEntity> Get(out int count, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool enableTracking = true, int offset = 0, int limit = -1)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (include != null) query = include(query);

            count = query.Count();

            if (orderBy != null)
                query = orderBy(query);

            //limit = -1: get all data
            if (limit != -1)
            {
                return query.Skip(offset).Take(limit);
            }

            return query.AsSplitQuery();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool enableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (include != null) query = include(query);

            if (orderBy != null)
                return orderBy(query).FirstOrDefault();
            return query.AsSplitQuery().FirstOrDefault();
        }

        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public async Task Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            await Delete(entityToDelete);
        }

        public async Task Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }
        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }


        public async Task UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            _dbSet.AttachRange(entitiesToUpdate);
            foreach (var item in entitiesToUpdate)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }
        public virtual IEnumerable<TEntity> GetDataFromSqlRaw(string sqlQuery, bool enableTracking = false)
        {
            if (enableTracking == true)
                return _dbSet.FromSqlRaw(sqlQuery).AsTracking().ToList();

            return _dbSet.FromSqlRaw(sqlQuery).AsNoTracking().ToList();
        }

        public List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, object[] parameter = null)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.Parameters.AddRange(parameter);
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }

    }
}
