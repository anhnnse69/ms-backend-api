using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Common.Contracts.Interfaces;

public interface IUnitOfWork<TContext> : IDisposable
    where TContext : DbContext
{
    Task<int> CommitAsync();
}
