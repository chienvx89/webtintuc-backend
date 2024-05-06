using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Infrastructure.Contexts;
using Website.Infrastructure.Repositories;

namespace Website.Infrastructure.IRepositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private Dictionary<(Type type, string name), object> _repositories;

        private readonly WebContext _context;

        public UnitOfWork(WebContext context)
        {
            _context = context;
        }

        private bool _disposed = false;
        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal object GetOrAddRepository(Type type, object repo)
        {
            if (_repositories == null) _repositories = new Dictionary<(Type type, string Name), object>();

            if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;
            _repositories.Add((type, repo.GetType().FullName), repo);
            return repo;
        }

        public GenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return (GenericRepository<TEntity>)GetOrAddRepository(typeof(TEntity), new GenericRepository<TEntity>(_context));
        }

        private GenericRepository<Version> _versionRepository;
        public GenericRepository<Version> VersionRepository
        {
            get
            {
                if (this._versionRepository == null)
                {
                    this._versionRepository = new GenericRepository<Version>(_context);
                }
                return _versionRepository;
            }
        }

    }
}
