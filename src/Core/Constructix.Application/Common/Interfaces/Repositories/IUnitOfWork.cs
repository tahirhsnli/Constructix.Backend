using Constructix.Application.Common.Interfaces.Repositories.Building;

namespace Constructix.Application.Common.Interfaces.Repositories;
public interface IUnitOfWork : IDisposable
{
    IBuildingWriteRepository Buildings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
