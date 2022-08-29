using Hospital.Integration.Business.Constants;
using Hospital.Integration.Business.Models;
using Hospital.Integration.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService) =>
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Post(string email, string password) =>
        await Task.FromResult(MountResult(await _authService.GenerateToken(email, password)));

    private IActionResult MountResult(AuthResponse response) => response.ResponseCode switch
    {
        ResponseCodes.Success => Ok(response),
        ResponseCodes.Unauthorized => StatusCode(StatusCodes.Status401Unauthorized),
        ResponseCodes.ErrorTryAgain => StatusCode(StatusCodes.Status500InternalServerError),
        ResponseCodes.ServiceUnavailable => StatusCode(StatusCodes.Status503ServiceUnavailable),
        _ => BadRequest(response),
    };
}
