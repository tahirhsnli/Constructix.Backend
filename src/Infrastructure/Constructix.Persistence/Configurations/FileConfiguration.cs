using Constructix.Domain.Entities.File;

namespace Constructix.Persistence.Configurations;
public class FileConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        // 1. Cədvəl adı
        builder.ToTable("Files");

        // 2. Primary Key
        builder.HasKey(x => x.Id);

        // 3. TPH (Table Per Hierarchy) Miras Tənzimləməsi
        // Bazada "FileCategory" adlı sütun yaranacaq və klassın tipini saxlayacaq
        builder.HasDiscriminator<string>("FileCategory")
            .HasValue<FileEntity>("BaseFile")
            .HasValue<ProfileImage>("UserProfileImage");

        // 4. Fayl Məlumatları
        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Storage)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false); // ASCII (varchar) olaraq saxlayır, yaddaşa qənaət edir

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>(); // Enum-u (0, 1, 2...) kimi saxlayır

        // 5. Index-lər
        builder.HasIndex(x => x.FileName);
        builder.HasIndex(x => x.Type); // Tipə görə filtrasiya çox olacaq (məs: ancaq şəkilləri gətir)
    }
}
