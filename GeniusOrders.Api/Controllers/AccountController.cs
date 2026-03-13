using GeniusOrders.Api.Features.Users.Commands.Login;
using GeniusOrders.Api.Features.Users.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeniusOrders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
    {
      
        
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("تم تسجيل المستخدم بنجاح.");
            }
            return BadRequest("فشل تسجيل المستخدم.");
        
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command)
    {

        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return Unauthorized(result);
        }
        return Ok(result);

    }

}