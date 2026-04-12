using Constructix.Application.Features.Buildings.Commands.CreateBuilding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Constructix.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BuildingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BuildingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBuildingCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result); // Yeni yaranan Guid Id-sini qaytaracaq
    }
}
