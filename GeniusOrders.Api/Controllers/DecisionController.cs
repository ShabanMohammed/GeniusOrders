using GeniusOrders.Api.Features.CreateDecision;
using GeniusOrders.Api.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using GeniusOrders.Api.Features.GetByIdQuery;
using AutoMapper;
using GeniusOrders.Api.Features.GetAllQuery;
using GeniusOrders.Api.Features.UpdateDecision;
namespace GeniusOrders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecisionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DecisionController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;

    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] DecisionDto decisionDto)
    {


        try
        {
            var command = new CreateDecisionComand
            (
                decisionDto.DecisionNumber,
                decisionDto.DecisionYear,
                decisionDto.DecisionDate,
                decisionDto.Content,
                decisionDto.Attachment
            );
            var id = await _mediator.Send(command);

            // تم استخدام nameof(GetById) بدلاً من النص الثابت لضمان سلامة الكود
            return CreatedAtAction(nameof(GetById), new { Id = id }, id);
        }
        catch (ValidationException ex)
        {

            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"حدث خطأ داخلي في الخادم: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        try
        {
            var result = await _mediator.Send(new GetDecisionByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"حدث خطأ أثناء معالجة الطلب{ex.Message}");
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllDecisionQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"حدث خطأ أثناء معالجة الطلب{ex.Message}");
        }
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdateDecision(int id, [FromBody] DecisionDto decisionDto)
    {
        if (decisionDto == null)
            return BadRequest("البيانات المُرسلة غير صحيحة");

        var exiteDecision = await _mediator.Send(new GetDecisionByIdQuery(id));
        if (exiteDecision is null)
            return NotFound($"القرار برقم {id} غير موجود");

        var command = new UpdateDecisionCommand
        (
            id,
            decisionDto.DecisionNumber,
            decisionDto.DecisionYear,
            decisionDto.DecisionDate,
            decisionDto.Content,
            decisionDto.Attachment
        );
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (ValidationException ex)
        {

            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"حدث خطأ داخلي في الخادم: {ex.Message}");
        }

    }

}

