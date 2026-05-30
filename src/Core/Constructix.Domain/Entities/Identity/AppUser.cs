using Constructix.Domain.Entities.File;

namespace Constructix.Domain.Entities.Identity;
/// <summary>
/// Constructix sistemində istifadəçini təmsil edən əsas obyekt.
/// </summary>
public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    /// <summary> İstifadəçinin bağlı olduğu MTK identifikatoru. SuperAdmin üçün null, digərləri üçün mütləqdir. </summary>
    //public Guid? BuildingId { get; set; }

    /// <summary> İstifadəçinin əsas fəaliyyət növü. Biznes məntiqində sürətli filtrasiya üçün istifadə olunur. </summary>
    public UserType UserType { get; set; }

    /// <summary> Hesabın aktivlik vəziyyəti. Məsələn: İşdən çıxan işçi və ya binadan köçən sakin deaktiv edilir. </summary>
    public bool IsActive { get; set; } = true;

    /// <summary> İstifadəçinin profil şəkli URL-i. Sakinlər və texniki heyətin tanınması üçün vacibdir. </summary>
    public Guid? ProfileImageId { get; set; } // Foreign Key

    /// <summary> Qeydiyyat tarixi. Statistika və hesabatlıq üçün. </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary> Məlumatların son dəyişdirilmə vaxtı. </summary>
    public DateTime? UpdatedAt { get; set; }

    // Navigation Properties (Biznes əlaqələri)
    public virtual ProfileImage? ProfileImage { get; set; } // Navigation
    public virtual ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new List<RefreshTokenEntity>();
}