using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pic.Application;

namespace Pic.Apresentation;
[ApiController]
[Route("[controller]")]
public class BalanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapster;

    public BalanceController(IMediator mediator, IMapper mapster)
    {
        _mediator = mediator;
        _mapster = mapster;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddBalanceInput model, CancellationToken cancellationToken)
    {
        var command = _mapster.Map<AddBalanceCommand>(model);
        var result = await _mediator.Send(command, cancellationToken);

        if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
