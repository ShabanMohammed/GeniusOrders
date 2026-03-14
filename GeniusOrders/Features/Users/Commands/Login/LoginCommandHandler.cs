using GeniusOrders.Api.Entities;
using GeniusOrders.Api.Features.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeniusOrders.Features.Users.Commands.Login;

public class LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService) : IRequestHandler<LoginCommand, AuthResponse>
{



    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return new AuthResponse(false, null, "اسم المستخدم أو كلمة المرور غير صحيحة.");
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.CreateToken(user, roles);
        return new AuthResponse(true, token, "تم تسجيل الدخول بنجاح.");
    }
}