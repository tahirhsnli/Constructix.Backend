using Constructix.Application.Abstractions.Repositories.Building;

namespace Constructix.Application.Abstractions.Repositories;
public interface IUnitOfWork : IDisposable
{
    IBuildingWriteRepository Buildings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
