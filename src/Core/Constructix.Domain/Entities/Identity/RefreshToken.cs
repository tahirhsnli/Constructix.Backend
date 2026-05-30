namespace Constructix.Domain.Entities.Identity;
public class RefreshTokenEntity : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string CreatedByIp { get; set; } = string.Empty;

    public bool IsRevoked { get; set; }
    public DateTime? RevokedDate { get; set; } // Adını bir az dəqiqləşdirdik
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; } // Təhlükəsizlik üçün: bu tokeni hansı yeni token əvəzlədi?

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!; // Sənin Identity-dəki AppUser klassın
}