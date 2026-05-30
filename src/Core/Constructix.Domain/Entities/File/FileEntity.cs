namespace Constructix.Domain.Entities.File;
public class FileEntity : BaseEntity
{
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Storage { get; set; } = string.Empty;
    public DocumentType Type { get; set; } // Məsələn: ProfileImage, IdentityCard və s.
}
