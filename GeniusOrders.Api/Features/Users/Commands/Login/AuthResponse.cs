namespace GeniusOrders.Api.Features.Users.Commands.Login;

public record AuthResponse(
    bool Success,
    string? Token = null,
    string? Message = null
);
