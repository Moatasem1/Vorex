using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.Interfaces;

namespace Vorex.Infrastructure.Persistence.Repositories.interfaces;

public interface IUnitOfWork :IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
