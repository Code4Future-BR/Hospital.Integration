using Hospital.Integration.Business.Constants;
using Hospital.Integration.Business.Models;
using Hospital.Integration.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers.V1;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService) =>
        _departmentService = departmentService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetDepartmentsByParamAsync(string request)
    {
        return await Task.FromResult(
            MountResult(await _departmentService.GetByParamAsync(request)));
    }

    private IActionResult MountResult(BaseResponse response) => response.ResponseCode switch
    {
        ResponseCodes.Success => Ok(response),
        ResponseCodes.Nonexistent => BadRequest(response),
        ResponseCodes.ErrorTryAgain => StatusCode(StatusCodes.Status500InternalServerError),
        ResponseCodes.ServiceUnavailable => StatusCode(StatusCodes.Status503ServiceUnavailable),
        _ => BadRequest(response),
    };
}
