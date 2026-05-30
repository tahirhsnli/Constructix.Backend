namespace Constructix.Application.Abstractions.Services;
public interface ITokenService
{
    Result<string, DomainError> CreateAccessToken(AppUser user, IList<string> roles);

    // Refresh tokenin özü sadəcə random stringdir, 
    // amma bizə onun neçə gün keçərli olacağını bilmək üçün DeviceType lazımdır.
    Result<string, DomainError> CreateRefreshToken(DeviceType deviceType);
}
