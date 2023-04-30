using System.Threading.Tasks;
using dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WalletsController : ControllerBase
{
    private readonly IMediator _mediator;
    public WalletsController(IMediator mediator) => _mediator = mediator;
    [HttpGet]
    public Task<GetWalletsInfo.Response> List([FromQuery] GetWalletsInfo.Request request) => _mediator.Send(request);
}
