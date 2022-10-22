using Hospital.Integration.Api.Factory.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class ProfessionalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfessionalsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ProfessionalsByParamAsync(string request)
    {
        var professionalsQuery = ProfessionalFactory.FromGet(request);

        var professionals = await _mediator.Send(professionalsQuery);
        return await Task.FromResult(Ok(professionals));
    }
}
