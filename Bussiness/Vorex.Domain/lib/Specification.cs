using System.Linq.Expressions;
using Vorex.Domain.lib.Interfaces;

namespace Vorex.Domain.lib;

public class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; protected set; }
}