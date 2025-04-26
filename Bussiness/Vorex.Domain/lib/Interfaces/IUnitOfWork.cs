using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.Interfaces;

namespace Vorex.Infrastructure.Persistence.Repositories.interfaces;

public interface IUnitOfWork :IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
