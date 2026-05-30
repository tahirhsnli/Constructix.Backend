

using Constructix.Application.Features.AppUsers.Login;
using Constructix.Application.Features.AppUsers.Register;

namespace Constructix.Application.Abstractions.Services;
public interface IAuthService
{
    Task<RegisterUserCommandResponse> RegisterAsync(RegisterUserCommandRequest request);
    Task<LoginUserCommandResponse> LoginAsync(LoginUserCommandRequest request);
}
