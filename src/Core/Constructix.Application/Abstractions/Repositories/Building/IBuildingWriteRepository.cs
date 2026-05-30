namespace Constructix.Application.Abstractions.Repositories.Building;
public interface IBuildingWriteRepository
{
    // Async olan hər yerə token əlavə edirik
    Task AddAsync(Domain.Entities.Building building, CancellationToken cancellationToken = default);

    // Update və Delete adətən asinxron olmur (çünki sadəcə context-də state dəyişir),
    // amma ehtiyac olsa bura da əlavə edilə bilər.
    void Update(Domain.Entities.Building building);
    void Delete(Domain.Entities.Building building);
}