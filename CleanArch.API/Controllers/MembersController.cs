using CleanArch.Application.Members.Commands;
using CleanArch.Application.Members.Queries;
using CleanArch.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var query = new GetMemberByIdQuery { Id = id };
        var member = await _mediator.Send(query);

        return member != null ? Ok(member) : NotFound("Member not found.");
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var query = new GetMembersQuery();
        var members = await _mediator.Send(query);

        return Ok(members);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(CreateMemberCommand command)
    {
        var member = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, UpdateMemberCommand command)
    {
        command.Id = id;
        var member = await _mediator.Send(command);

        return member != null ? NoContent() : NotFound("Member not found."); ;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id, DeleteMemberCommand command)
    {
        command.Id = id;
        var member = await _mediator.Send(command);

        return member != null ? Ok() : NotFound("Member not found.");
    }
}
