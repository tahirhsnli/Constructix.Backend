namespace Constructix.Infrastructure.Services;

public class TokenService(IOptions<JwtSettings> jwtOptions) : ITokenService
{
    private readonly JwtSettings _settings = jwtOptions.Value;

    public Result<string, DomainError> CreateAccessToken(AppUser user, IList<string> roles)
    {
        try
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Rolları əlavə edirik
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            return DomainError.InfrastructureError($"JWT Generation Failed: {ex.Message}");
        }
    }

    public Result<string, DomainError> CreateRefreshToken(DeviceType deviceType)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        string baseToken = Convert.ToBase64String(randomNumber);

        // Sənin ayırd etmə məntiqin:
        return deviceType switch
        {
            DeviceType.Web => $"W_{baseToken}",
            DeviceType.Mobile => $"M_{baseToken}",
            _ => baseToken
        };
    }
}
