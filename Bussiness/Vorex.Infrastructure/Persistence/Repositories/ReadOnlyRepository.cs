using Microsoft.EntityFrameworkCore;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;

namespace Vorex.Infrastructure.Persistence.Repositories;

public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class,IAggregateRoot
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public ReadOnlyRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public IQueryable<TEntity> Find(ISpecification<TEntity> specification)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (specification?.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        return query;
    }
}
