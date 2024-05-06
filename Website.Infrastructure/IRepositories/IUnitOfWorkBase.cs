using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Infrastructure.IRepositories
{
    public interface IUnitOfWorkBase<WebContext> : IDisposable where WebContext : DbContext
    {
        Task<int> CommitAsync();

        Task SaveChangeAsync();
    }
}
