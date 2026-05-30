namespace Constructix.Persistence.Configurations.Identity;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        // 1. Şəxsi Məlumatlar
        builder.Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();

        // 2. Identity Sahələri (Email və UserName)
        builder.Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.UserName)
            .HasMaxLength(50)
            .IsRequired();

        // 3. Biznes Sahələri (Enum və Statuslar)
        builder.Property(u => u.UserType)
            .IsRequired()
            .HasConversion<int>(); // Bazada int kimi saxlamaq performans üçün yaxşıdır

        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("timezone('utc', now())");

        // 4. ƏLAQƏ: ProfileImage (One-to-One)
        // ProfileImageUrl-i sildik, yerinə FileEntity əlaqəsi gəldi
        builder.HasOne(u => u.ProfileImage)
                       .WithOne(pi => pi.User)
                       // Burada <AppUser> yazaraq mühərrikə deyirik ki, get məhz AppUser-in içindəki ProfileImageId-ni götür, özündən "1" uydurma!
                       .HasForeignKey<AppUser>(u => u.ProfileImageId)
                       .OnDelete(DeleteBehavior.SetNull);

        // 5. ƏLAQƏ: Refresh Tokens (One-to-Many)
        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade); // İstifadəçi silinsə, bütün sessiyaları silinsin

        // 6. ƏLAQƏ: Roles (Many-to-Many via AppUserRole)
        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        // 7. Cədvəl adı
        builder.ToTable("Users");
    }
}