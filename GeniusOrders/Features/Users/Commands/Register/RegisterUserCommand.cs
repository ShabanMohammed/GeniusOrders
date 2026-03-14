using MediatR;

namespace GeniusOrders.Features.Users.Commands.Register;

public record RegisterUserCommand(
    string FullName,
    string UserName,
    string Department,
    string Password
) : IRequest<bool>;
