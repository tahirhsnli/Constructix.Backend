namespace Constructix.Application.Abstractions.Repositories.Building;

public interface IBuildingReadRepository
{
    Task<Domain.Entities.Building?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.Building>> GetAllAsync(bool tracking = false, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
}

// bruda eyni adlandirma var deyismek lazimdi