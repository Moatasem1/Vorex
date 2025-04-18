using System.Linq.Expressions;

namespace Vorex.Domain.lib.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}
