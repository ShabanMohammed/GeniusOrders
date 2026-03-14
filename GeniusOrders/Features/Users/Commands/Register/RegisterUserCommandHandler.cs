using GeniusOrders.Api.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeniusOrders.Features.Users.Commands.Register;

public class RegisterUserCommandHandler(UserManager<User> userManager) : IRequestHandler<RegisterUserCommand, bool>
{


    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FullName = request.FullName,
            UserName = request.UserName,
            Department = request.Department
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return true;
        }
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception($"فشل تسجيل المستخدم: {errors}");
    }
}