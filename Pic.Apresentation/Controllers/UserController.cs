using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pic.Application;

namespace Pic.Apresentation;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapster;

    public UserController(IMediator mediator, IMapper mapster)
    {
        _mediator = mediator;
        _mapster = mapster;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserInput model, CancellationToken cancellationToken)
    {
        var command = _mapster.Map<CreateUserCommand>(model);
        var result = await _mediator.Send(command, cancellationToken);

        if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
