
using Constructix.Domain.Entities.File;

namespace Constructix.Persistence.Configurations;
public class ProfileImageConfiguration : IEntityTypeConfiguration<ProfileImage>
{
    public void Configure(EntityTypeBuilder<ProfileImage> builder)
    {
        // 1. IsAvatar sahəsi üçün default dəyər
        builder.Property(x => x.IsAvatar)
            .HasDefaultValue(false);
    }
}