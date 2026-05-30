using Constructix.Domain.Entities.Identity;

namespace Constructix.Persistence.Configurations.Identity;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        // 1. Cədvəl adı (Opsional, amma Identity cədvəlləri ilə qarışmaması üçün yaxşıdır)
        builder.ToTable("UserRefreshTokens");

        // 2. Primary Key
        builder.HasKey(x => x.Id);

        // 3. Token üçün Index və Validasiya
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(250);

        // Təhlükəsizlik və sürət üçün: Token üzərindən axtarış çox olacaq, ona görə Unique Index şərtdir
        builder.HasIndex(x => x.Token)
            .IsUnique();

        // 4. Tarixlər üzərində dəqiqlik (PostgreSQL işlədirsənsə timestamptz məsləhətdir)
        builder.Property(x => x.Expiration)
            .IsRequired();

        builder.Property(x => x.Created).HasDefaultValueSql("timezone('utc', now())"); // MSSQL üçün. PostgreSQL üçün: "now() at time zone 'utc'"

        // 5. IP Ünvanları (IPv6 dəstəyi üçün 50-100 arası ideal ölçüdür)
        builder.Property(x => x.CreatedByIp)
            .HasMaxLength(100)
            .IsUnicode(false); // IP-də xüsusi simvollar olmur, yaddaşa qənaət

        builder.Property(x => x.RevokedByIp)
            .HasMaxLength(100)
            .IsUnicode(false);

        // 6. Əlaqəli Token (ReplacedByToken)
        builder.Property(x => x.ReplacedByToken)
            .HasMaxLength(250);

        // 7. Münasibət (Relationship) - DÜZƏLDİLMİŞ VARIANT
        // Bir istifadəçinin çoxlu tokeni ola bilər və bu əlaqə AppUser-dəki kolleksiyaya bağlanır!
        builder.HasOne(x => x.User)
            .WithMany(u => u.RefreshTokens) // <--- MÖTƏRİZƏNİN İÇİNƏ BU KOLLEKSİYANI YAZIRIQ!
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 8. Performance Index
        // UserId üzərindən filterləmə (məsələn, "User-in bütün aktiv sessiyalarını gətir") çox olacaq
        builder.HasIndex(x => x.UserId);

        // Expiration üzərində Index (Bazadakı köhnə tokenləri təmizləyən Background Job üçün)
        builder.HasIndex(x => x.Expiration);
    }
}