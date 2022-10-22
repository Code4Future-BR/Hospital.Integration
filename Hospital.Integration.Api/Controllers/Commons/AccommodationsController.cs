using Hospital.Integration.Api.Factory.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class AccommodationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccommodationsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AccommodationByParamAsync(string request)
    {
        var accommodationsQuery = AccommodationFactory.FromGet(request);

        var accommodations = await _mediator.Send(accommodationsQuery);
        return await Task.FromResult(Ok(accommodations));
    }
}
