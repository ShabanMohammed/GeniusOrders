using GeniusOrders.Api.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeniusOrders.Api.Features.Users.Commands.Login;

public class LoginCommandHandler(UserManager<User> userManager) : IRequestHandler<LoginCommand, AuthResponse>
{



    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            return new AuthResponse(false, null, "اسم المستخدم أو كلمة المرور غير صحيحة.");
        }
        if (!await userManager.CheckPasswordAsync(user, request.Password))
        {
            return new AuthResponse(false, null, "اسم المستخدم أو كلمة المرور غير صحيحة.");
        }


        return new AuthResponse(true, "token-placeholder", "تم تسجيل الدخول بنجاح.");
    }
}