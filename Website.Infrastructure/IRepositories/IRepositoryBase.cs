using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Abstract;
using Website.Infrastructure.Contexts;

namespace Website.Infrastructure.IRepositories
{
    public interface IRepositoryBase<TEntity, TPrimaryKey, Context>
        where Context : DbContext
        where TEntity : class
    {
        /// <summary>
        /// GetAll() = dbContext.Tables
        /// </summary>
        /// <param name="trackChanges"></param>
        /// 
        /// <returns></returns>
        IQueryable<TEntity> GetAll(bool trackChanges = false);
        /// <summary>
        /// GetAll() = dbContext.Tables
        /// </summary>
        /// <param name="trackChanges"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false,
            params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync(TPrimaryKey Id);
        TEntity Create(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
        IList<TEntity> CreateList(IEnumerable<TEntity> entities);

        Task<IList<TEntity>> CreateListAsync(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        IList<TEntity> UpdateList(IEnumerable<TEntity> entities);

        Task<IList<TEntity>> UpdateListAsync(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);
        void Delete(TPrimaryKey id);

        Task DeleteAsync(TPrimaryKey id);

        void DeleteList(IEnumerable<TEntity> entities);

        Task DeleteListAsync(IEnumerable<TEntity> entities);

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();

        Task SaveAsync();

        void Save();
    }

    public interface IRepositoryBase<TEntity, Context> : IRepositoryBase<TEntity, int, Context>
      where TEntity : class
      where Context : DbContext
    {

    }

    public interface IRepository<TEntity, TPrimaryKey, Context> : IRepositoryBase<TEntity, TPrimaryKey, Context>
        where TEntity : class
        where Context : DbContext
    {

    }

    public interface IRepository<TEntity, Context> : IRepositoryBase<TEntity, int, Context>
        where TEntity : class
        where Context : DbContext
    {

    }

    public interface IRepository<TEntity> : IRepositoryBase<TEntity, int, WebContext> where TEntity : class
    {

    }
}
