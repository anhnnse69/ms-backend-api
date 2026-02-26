using Microsoft.EntityFrameworkCore;
using MS.Infrastructure.Common.Contracts.Interfaces;

namespace MS.Infrastructure.Common.Contracts;

public class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private bool _disposed = false;
    private readonly TContext _context;

    public UnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _context.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
}
