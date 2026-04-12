namespace Constructix.Persistence.Repositories.Building;
public class BuildingReadRepository : IBuildingReadRepository
{
    private readonly ApplicationDbContext _context;
    public BuildingReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Domain.Entities.Building?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Buildings.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<List<Domain.Entities.Building>> GetAllAsync(bool tracking = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Buildings.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.ToListAsync(cancellationToken);
    }
    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        // ! işarəsi ilə: Əgər heç bir bina tapılmasa (Any = false), deməli ad unikaldir (True)
        return !await _context.Buildings.AnyAsync(x => x.Name == name, cancellationToken);
    }
}