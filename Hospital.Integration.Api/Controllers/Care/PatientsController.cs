using Hospital.Integration.Api.Factory.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PatientsByParamAsync(string request)
    {
        var patientsQuery = PatientFactory.FromGet(request);

        var patients = await _mediator.Send(patientsQuery);
        return await Task.FromResult(Ok(patients));
    }
}
