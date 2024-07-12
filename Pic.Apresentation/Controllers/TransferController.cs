using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pic.Application;

namespace Pic.Apresentation;

[ApiController]
[Route("[controller]")]
public class TransferController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapster;

    public TransferController(IMediator mediator, IMapper mapster)
    {
        _mediator = mediator;
        _mapster = mapster;
    }

    [HttpPost]
    public async Task<IActionResult> Transfer(TransferCommand model, CancellationToken cancellationToken)
    {
        var command = _mapster.Map<TransferCommand>(model);
        var result = await _mediator.Send(command, cancellationToken);

        if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return BadRequest(result);
        }

         if(result.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return StatusCode(403, result.Message);
        }
        return Ok(result);
    }
}
