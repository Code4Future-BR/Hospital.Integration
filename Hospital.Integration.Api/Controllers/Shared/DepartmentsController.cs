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

    [HttpGet("{id}", Name = nameof(GetDepartmentsByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetDepartmentsByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out _))
        {
            return await Task.FromResult(BadRequest("Invalid Id"));
        }

        var department = await _departmentService.GetByIdAsync(id);
        if (department == null)
        {
            return await Task.FromResult(NotFound());
        }

        return await Task.FromResult(Ok(department));
    }

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateDepartmentAsync(string request)
    {
        var (ret, id) = await _departmentService.CreateAsync(request);
        return await Task.FromResult(CreatedAtRoute(
         routeName: nameof(GetDepartmentsByIdAsync),
         routeValues: new { id },
         value: request));
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
