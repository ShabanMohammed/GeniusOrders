using GeniusOrders.Api.Features.CreateDecision;
using GeniusOrders.Api.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using GeniusOrders.Api.Features.GetByIdQuery;
using AutoMapper;
using GeniusOrders.Api.Features.GetAllQuery;
using GeniusOrders.Api.Features.UpdateDecision;
using GeniusOrders.Api.Features.DeleteDecision;
using GeniusOrders.Api.Features.GetDecisions;
using Microsoft.AspNetCore.Authorization;
namespace GeniusOrders.Controllers;

[Authorize]
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

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {

        var result = await _mediator.Send(new GetDecisionByIdQuery(id));
        if (result == null)
            return NotFound();
        return Ok(result);


    }

    [HttpGet()]
    public async Task<ActionResult> GetAll([FromQuery] GetDecisionsQuery query)
    {

        var result = await _mediator.Send(query);
        return Ok(result);

    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdateDecision(int id, [FromBody] DecisionDto decisionDto)
    {
        if (decisionDto == null)
            return BadRequest("البيانات المُرسلة غير صحيحة");



        var command = new UpdateDecisionCommand
        (
            id,
            decisionDto.DecisionNumber,
            decisionDto.DecisionYear,
            decisionDto.DecisionDate,
            decisionDto.Content,
            decisionDto.Attachment
        );

        await _mediator.Send(command);
        return NoContent();


    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteDecision(int id)
    {

        var command = new DeleteDecisionCommand(id);
        await _mediator.Send(command);
        return NoContent();

    }
}

