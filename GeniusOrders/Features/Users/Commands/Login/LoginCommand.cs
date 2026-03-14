using MediatR;

namespace GeniusOrders.Features.Users.Commands.Login;

public record LoginCommand(
    string UserName,
    string Password,
    bool RememberMe
) : IRequest<AuthResponse>;
