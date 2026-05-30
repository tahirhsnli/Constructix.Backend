using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Constructix.Application.Abstractions.Services;

using Microsoft.AspNetCore.Identity;

namespace Constructix.Application.Features.AppUsers.Login;
public record LoginUserCommandRequest(
    string EmailOrUserName,
    string Password,
    int DeviceType // 0 = Web, 1 = Mobile (Enum sırasına görə)
) : IRequest<LoginUserCommandResponse>;

public record LoginUserCommandResponse(
    bool IsAuthenticated,
    string Message,
    string? AccessToken = null,
    string? RefreshToken = null,
    DateTime? RefreshTokenExpires = null
);

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService) => _authService = authService;

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request);
    }
}