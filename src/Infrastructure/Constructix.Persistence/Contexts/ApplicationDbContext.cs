using Constructix.Domain.Entities.File;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Constructix.Persistence.Contexts;
// Buranı IdentityDbContext olaraq dəyişirik və istifadə etdiyin custom AppUser, AppRole tiplərini bəyan edirik
// Buranı IdentityDbContext olaraq dəyişirik və istifadə etdiyin custom AppUser, AppRole tiplərini bəyan edirik
public class ApplicationDbContext : IdentityDbContext<
    AppUser,
    AppRole,
    Guid,
    IdentityUserClaim<Guid>,
    AppUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // --- Biznes Cədvəlləri ---
    public DbSet<Building> Buildings => Set<Building>();

    // --- Fayl və Şəkil Sistemi (TPH olduğu üçün FileEntity kifayətdir) ---
    public DbSet<FileEntity> Files => Set<FileEntity>();

    // --- Təhlükəsizlik ---
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Çox Vacib: Bu base çağırışı artıq IdentityDbContext-in daxili cədvəllərini (UserRoles və s.) modelə daxil edir!
        base.OnModelCreating(modelBuilder);

        // Sənin yazdığın bütün IEntityTypeConfiguration klasslarını tətbiq edir
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}