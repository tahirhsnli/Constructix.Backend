namespace Constructix.Persistence.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("Buildings");
            builder.HasKey(b => b.Id);

            // 1. Əsas Property-lər
            builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Description).HasMaxLength(1000);
            builder.Property(b => b.TotalArea).HasPrecision(18, 2);
            builder.Property(b => b.Status).HasConversion<int>().IsRequired();

            // 2. Value Object (Address) - Cəmi 5 sətir, bölməyə dəyməz
            builder.OwnsOne(b => b.Address, a =>
            {
                a.Property(p => p.Street).HasColumnName("Address_Street").HasMaxLength(150);
                a.Property(p => p.City).HasColumnName("Address_City").HasMaxLength(100);
                a.Property(p => p.District).HasColumnName("Address_District").HasMaxLength(100);
            });

            // 3. Qlobal Filtrlər
            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
