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
using Website.Infrastructure.IRepositories;

namespace Website.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity, TPrimaryKey, WebContext> : IRepositoryBase<TEntity, TPrimaryKey, WebContext>, IRepository<TEntity, TPrimaryKey, WebContext>
        where TEntity : class
        where WebContext : DbContext
    {
        private readonly WebContext _WebContext;
        private readonly IUnitOfWorkBase<WebContext> _unitOfWork;

        public RepositoryBase(WebContext webContext, IUnitOfWorkBase<WebContext> unitOfWork)
        {
            _WebContext = webContext ?? throw new ArgumentNullException(nameof(webContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
        }

        public IQueryable<TEntity> GetAll(bool trackChanges = false)
        {
            return !trackChanges ? _WebContext.Set<TEntity>().AsNoTracking() : _WebContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll(trackChanges);
            if (includeProperties == null || includeProperties.Count() <= 0)
            {
                return query;
            }

            foreach (var includePropertie in includeProperties)
            {
                query = query.Include(includePropertie);
            }

            return query;
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false)
        {
            return !trackChanges ? _WebContext.Set<TEntity>().Where(expression).AsNoTracking() : _WebContext.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = FindByCondition(expression, trackChanges);
            if (includeProperties == null || includeProperties.Count() <= 0)
            {
                return query;
            }

            foreach (var includePropertie in includeProperties)
            {
                query = query.Include(includePropertie);
            }
            //query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public TEntity Create(TEntity entity)
        {
            return _WebContext.Set<TEntity>().Add(entity).Entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entityEntry = await _WebContext.Set<TEntity>().AddAsync(entity);
            var entiyReturn = entityEntry.Entity;
            //var entiyReturn = (await _WebContext.Set<TEntity>().AddAsync(entity)).Entity;
            await SaveChangesAsync();
            return entiyReturn;
        }

        public IList<TEntity> CreateList(IEnumerable<TEntity> entities)
        {
            _WebContext.Set<TEntity>().AddRange(entities);
            return entities.ToList();
        }

        public async Task<IList<TEntity>> CreateListAsync(IEnumerable<TEntity> entities)
        {
            await _WebContext.Set<TEntity>().AddRangeAsync(entities);
            await SaveChangesAsync();
            return entities.ToList();
        }

        public TEntity Update(TEntity entity)
        {
            var entry = _WebContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return entity;
            }

            _WebContext.Set<TEntity>().Attach(entity);

            _WebContext.Entry(entity).State = EntityState.Modified;

            return entity;

            //if (_WebContext.Entry(entity).State == EntityState.Unchanged)
            //    return entity;
            //TEntity exist = _WebContext.Set<TEntity>().Find(entity.Id);
            //_WebContext.Entry(exist).CurrentValues.SetValues(entity);
            //return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            //if (_WebContext.Entry(entity).State == EntityState.Unchanged)
            //    return entity;
            //TEntity exist = _WebContext.Set<TEntity>().Find(entity.Id);
            //_WebContext.Entry(exist).CurrentValues.SetValues(entity);
            //await _WebContext.SaveChangesAsync();
            //return entity;
            entity = Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public IList<TEntity> UpdateList(IEnumerable<TEntity> entities)
        {
            _WebContext.Set<TEntity>().UpdateRange(entities);
            return entities.ToList();
        }

        public async Task<IList<TEntity>> UpdateListAsync(IEnumerable<TEntity> entities)
        {
            _WebContext.Set<TEntity>().UpdateRange(entities);
            await SaveChangesAsync();
            return entities.ToList();
        }

        public void Delete(TEntity entity)
        {
            _WebContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteList(IEnumerable<TEntity> entities)
        {
            _WebContext.Set<TEntity>().RemoveRange(entities);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _WebContext.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
            if (entity != null)
            {
                Delete(entity);
                return;
            }
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            await SaveChangesAsync();
        }

        public async Task DeleteListAsync(IEnumerable<TEntity> entities)
        {
            _WebContext.Set<TEntity>().RemoveRange(entities);
            await SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _WebContext.Database.BeginTransactionAsync();
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _WebContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _WebContext.Database.RollbackTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await _WebContext.SaveChangesAsync();
        }

        public void Save()
        {
            _WebContext.SaveChanges();
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            var idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }


    public class RepositoryBase<TEntity, TDbContext> : RepositoryBase<TEntity, int, TDbContext>,
                                                       IRepositoryBase<TEntity, int, TDbContext>,
                                                       IRepository<TEntity, int, TDbContext>,
                                                       IRepository<TEntity, TDbContext>

        where TEntity : class
        where TDbContext : DbContext
    {
        public RepositoryBase(TDbContext dbContext, IUnitOfWorkBase<TDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }
    }

    public class RepositoryBase<TEntity> : RepositoryBase<TEntity, int, WebContext>,
                                                  IRepositoryBase<TEntity, int, WebContext>,
                                                  IRepository<TEntity, int, WebContext>,
                                                  IRepository<TEntity, WebContext>,
                                                  IRepository<TEntity>

       where TEntity : class
    {
        public RepositoryBase(WebContext dbContext, IUnitOfWorkBase<WebContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }
    }
}
