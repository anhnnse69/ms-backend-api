using Microsoft.EntityFrameworkCore;
using MS.Infrastructure.Common.Contracts.Interfaces;

namespace MS.Infrastructure.Common.Contracts;

public class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private readonly TContext _context;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public void Dispose() => _context.Dispose();

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
}
