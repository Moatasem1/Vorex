using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;

namespace Vorex.Infrastructure.Persistence.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public void Dispose()
    {
        context.Dispose();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
