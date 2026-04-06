namespace Constructix.Persistence.Contexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Building> Buildings => Set<Building>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Bütün Configuration-ları (BuildingConfiguration və s.) avtomatik tapır
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
