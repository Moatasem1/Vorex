using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;

namespace Vorex.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IEnumerable<TEntity> Find(ISpecification<TEntity> specification, bool track = false)
    {
        var query = ApplySpecification(specification);

        if (!track)
            query = query.AsNoTracking();

        query = query.Where(specification.Criteria);

        return query.ToList();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool track = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!track)
            query = query.AsNoTracking();

        query = query.Where(predicate);
        return query.ToList();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public bool Contains(ISpecification<TEntity> specification)
    {
        return ApplySpecification(specification).Any();
    }

    public bool Contains(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    public int Count(ISpecification<TEntity> specification)
    {
        return ApplySpecification(specification).Count();
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Count(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.AsEnumerable();
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