using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Constructix.Application.Abstractions.Services;
using Constructix.Application.Configurations;
using Constructix.Application.Features.AppUsers.Login;
using Constructix.Application.Features.AppUsers.Register;
using Constructix.Domain.Entities.Identity; // AppUser və RefreshTokenEntity üçün mütləq lazımdır
using Constructix.Domain.Enums;
using Constructix.Persistence.Contexts; // ApplicationDbContext üçün mütləq lazımdır

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // FirstOrDefaultAsync-in işləməsi üçün mütləq lazımdır
using Microsoft.Extensions.Options;

namespace Constructix.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<AppUser> userManager,
        ApplicationDbContext context,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<RegisterUserCommandResponse> RegisterAsync(RegisterUserCommandRequest request)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            UserType = (UserType)request.UserType,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.FirstOrDefault()?.Description ?? "Qeydiyyat xətası.";
            return new RegisterUserCommandResponse(false, error);
        }

        return new RegisterUserCommandResponse(true, "İstifadəçi uğurla qeydiyyatdan keçdi!");
    }

    public async Task<LoginUserCommandResponse> LoginAsync(LoginUserCommandRequest request)
    {
        // 1. İstifadəçi yoxlanışı
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == request.EmailOrUserName || u.UserName == request.EmailOrUserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return new LoginUserCommandResponse(false, "Məlumatlar yanlışdır!");

        if (!user.IsActive)
            return new LoginUserCommandResponse(false, "Hesabınız deaktiv edilib!");

        var roles = await _userManager.GetRolesAsync(user);

        // 2. Access Token yaradılması
        var accessTokenResult = _tokenService.CreateAccessToken(user, roles);
        if (!accessTokenResult.IsSuccess)
            return new LoginUserCommandResponse(false, "Access Token yaradıla bilmədi.");

        // 3. Refresh Token yaradılması (DeviceType ilə)
        var deviceType = (DeviceType)request.DeviceType;
        var refreshTokenResult = _tokenService.CreateRefreshToken(deviceType);
        if (!refreshTokenResult.IsSuccess)
            return new LoginUserCommandResponse(false, "Refresh Token yaradıla bilmədi.");

        // 4. Token-lərin dəyərlərini götürürük
        string accessToken = accessTokenResult.Value;
        string refreshTokenStr = refreshTokenResult.Value;

        // 5. Refresh Token Entity qurulması (_jwtSettings vasitəsilə strongly-typed idarə olunur)
        var refreshTokenEntity = new RefreshTokenEntity
        {
            Token = refreshTokenStr,
            UserId = user.Id,
            Expiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            Created = DateTime.UtcNow,
            CreatedByIp = "127.0.0.1",
            IsRevoked = false,
            RevokedDate = null,
            RevokedByIp = null,
            ReplacedByToken = null
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

        return new LoginUserCommandResponse(
            IsAuthenticated: true,
            Message: "Giriş uğurludur!",
            AccessToken: accessToken,
            RefreshToken: refreshTokenStr,
            RefreshTokenExpires: refreshTokenEntity.Expiration
        );
    }
}