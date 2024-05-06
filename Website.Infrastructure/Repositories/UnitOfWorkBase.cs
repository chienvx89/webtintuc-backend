using Microsoft.EntityFrameworkCore;
using Website.Infrastructure.IRepositories;

namespace Website.Infrastructure.Repositories
{
    public class UnitOfWorkBase<WebContext> : IUnitOfWorkBase<WebContext>
        where WebContext : DbContext
    {
        private readonly WebContext _webContext;

        public UnitOfWorkBase(WebContext webContext)
        {
            _webContext = webContext;
        }

        public Task<int> CommitAsync()
        {
            return _webContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _webContext.Dispose();
        }

        public async Task SaveChangeAsync()
        {
            await _webContext.SaveChangesAsync();
        }
    }
}
