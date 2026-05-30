namespace Constructix.Domain.Entities.Identity;
public class AppUserRole : IdentityUserRole<Guid>
{
    /// <summary>  
    /// Bu rolun istifadəçiyə təyin edildiyi vaxt. 
    /// Arxiv və audit məqsədləri üçün. </summary>
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    /// <summary> Rolu kim təyin etdi? (Opsional - Təhlükəsizlik auditi üçün). </summary>
    public Guid? AssignedBy { get; set; }

    /// <summary> İstifadəçinin bir neçə rolu varsa, hansının əsas olduğunu bildirir. </summary>
    public bool IsPrimary { get; set; } = false;

    // Navigation Properties
    // Bu əlaqələr fluent API ilə DbContext-də daha dərindən tənzimlənəcək.
    public virtual AppUser User { get; set; } = null!;
    public virtual AppRole Role { get; set; } = null!;
}
