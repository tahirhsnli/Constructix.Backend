namespace Constructix.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IBuildingWriteRepository? _buildingWriteRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    // Lazy loading ilə ancaq Write Repository-ni qaytarırıq
    public IBuildingWriteRepository Buildings =>
        _buildingWriteRepository ??= new BuildingWriteRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}