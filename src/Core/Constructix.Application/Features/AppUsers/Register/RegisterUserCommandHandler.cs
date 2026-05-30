
using Constructix.Application.Abstractions.Services;

using Microsoft.AspNetCore.Identity;

namespace Constructix.Application.Features.AppUsers.Register;
public record RegisterUserCommandRequest(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    int UserType
) : IRequest<RegisterUserCommandResponse>;

public record RegisterUserCommandResponse(bool Succeeded, string Message);

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(IAuthService authService) => _authService = authService;

    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request);
    }
}