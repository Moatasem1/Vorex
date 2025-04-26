using Microsoft.EntityFrameworkCore;
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

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        
        try
        {
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }        
    }
}
