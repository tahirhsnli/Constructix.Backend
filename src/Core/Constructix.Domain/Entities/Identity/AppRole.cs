namespace Constructix.Domain.Entities.Identity;
public class AppRole : IdentityRole<Guid>
{
    public AppRole() : base() { }
    public AppRole(string roleName) : base(roleName) { }

    /// <summary> Rolun biznes təyinatı (məs: "Bütün binaların lift təmirinə baxan heyət"). </summary>
    public string? Description { get; set; }

    /// <summary> Rolun yaradılma tarixi. </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary> Rolun son dəyişdirilmə vaxtı. </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary> Əgər true-dursa, bu rol sistem tərəfindən yaradılıb və silinə bilməz. </summary>
    public bool IsStatic { get; set; } = false;

    // Navigation Properties
    public virtual ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
}
