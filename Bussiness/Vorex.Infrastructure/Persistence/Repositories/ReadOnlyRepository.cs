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
        var query = ApplySpecification(specification);

        if (specification?.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        return query;
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        IQueryable<TEntity> query = _dbSet;

        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        foreach (var include in spec.Includes)
            query = query.Include(include);

        return query;
    }
}
