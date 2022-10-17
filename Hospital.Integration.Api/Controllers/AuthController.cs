using Hospital.Integration.Api.Factories.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(
        IMediator mediator) =>
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Post(string request)
    {
        var requestModel = AuthFactory.FromPost(request);
        if (requestModel == null)
        {
            return await Task.FromResult(StatusCode(StatusCodes.Status401Unauthorized));
        }

        var newToken = await _mediator.Send(requestModel);

        if (newToken is null)
        {
            return await Task.FromResult(StatusCode(StatusCodes.Status401Unauthorized));
        }

        return await Task.FromResult(Ok(newToken));
    }
}
