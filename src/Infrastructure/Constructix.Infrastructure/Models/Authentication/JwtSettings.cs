namespace Constructix.Infrastructure.Models.Authentication;

public record JwtSettings
{
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
    public int AccessTokenExpirationMinutes { get; init; }
}