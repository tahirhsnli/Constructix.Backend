namespace Constructix.Persistence.Configurations.Identity;
public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.Property(r => r.Name).HasMaxLength(50).IsRequired();
        builder.Property(r => r.NormalizedName).HasMaxLength(50).IsRequired();

        builder.ToTable("Roles");
    }
}