
namespace Constructix.Domain.Entities.File;
public class ProfileImage : FileEntity
{
    // Profil şəklinə özəl xüsusiyyət (məsələn: sistemin verdiyi standart avatardırsa true olacaq)
    public bool IsAvatar { get; set; } = false;
    public virtual AppUser? User { get; set; }

}
