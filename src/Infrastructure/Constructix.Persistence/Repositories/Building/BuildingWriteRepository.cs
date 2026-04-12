namespace Constructix.Persistence.Repositories.Building;
public class BuildingWriteRepository : IBuildingWriteRepository
{
    private readonly ApplicationDbContext _context;
    public BuildingWriteRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Domain.Entities.Building building, CancellationToken cancellationToken = default) => await _context.Buildings.AddAsync(building, cancellationToken);
    public void Update(Domain.Entities.Building building) => _context.Buildings.Update(building);
    public void Delete(Domain.Entities.Building building) => _context.Buildings.Remove(building);
}