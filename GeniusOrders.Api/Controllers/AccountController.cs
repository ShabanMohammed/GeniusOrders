using GeniusOrders.Api.Features.Users.Commands.Register;
using MediatR;
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

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("تم تسجيل المستخدم بنجاح.");
            }
            return BadRequest("فشل تسجيل المستخدم.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"حدث خطأ داخلي في الخادم: {ex.Message}");
        }
    }
}