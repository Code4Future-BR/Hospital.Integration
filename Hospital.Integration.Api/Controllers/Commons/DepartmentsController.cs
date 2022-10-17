using Hospital.Integration.Api.Factories;
using Hospital.Integration.Api.Factory.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Business.Constants;
using Hospital.Integration.Business.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers.V1;

[Route("api/[controller]")]
// [Authorize]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{request}", Name = nameof(DepartmentsByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DepartmentsByIdAsync(string request)
    {
        if (!Guid.TryParse(request, out _))
        {
            return await Task.FromResult(BadRequest("Invalid Id"));
        }

        var requestModel = RequestFactory.From(request);
        var department = await _mediator.Send(requestModel);

        if (department is null)
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
    public async Task<IActionResult> DepartmentsByParamAsync(string request)
    {
        var requestModel = RequestFactory.From(request);
        var departmentsQuery = DepartmentFactory.FromGet(requestModel);

        if (departmentsQuery is null)
        {
            return await Task.FromResult(NotFound());
        }

        var departments = await _mediator.Send(departmentsQuery);
        return await Task.FromResult(MountResult(departments));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DepartmentCreateAsync(string request)
    {
        var requestModel = RequestFactory.From(request);
        var departmentCreateCommand = DepartmentFactory.FromCreate(requestModel);

        if (departmentCreateCommand is null)
        {
            return await Task.FromResult(BadRequest("Invalid data"));
        }

        var id = await _mediator.Send(departmentCreateCommand);
        return await Task.FromResult(CreatedAtRoute(
         routeName: nameof(DepartmentsByIdAsync),
         routeValues: new { id },
         value: request));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DepartmentUpdateAsync(string request)
    {
        var requestModel = RequestFactory.From(request);
        var departmentUpdateCommand = DepartmentFactory.FromUpdate(requestModel);

        if (departmentUpdateCommand is null)
        {
            return await Task.FromResult(BadRequest("Invalid data"));
        }

        var id = await _mediator.Send(departmentUpdateCommand);
        return await Task.FromResult(CreatedAtRoute(
         routeName: nameof(DepartmentsByIdAsync),
         routeValues: new { id },
         value: request));
    }

    private IActionResult MountResult(ListResponse response) => response.ResponseCode switch
    {
        ResponseCodes.Success => Ok(response),
        ResponseCodes.Nonexistent => BadRequest(response),
        ResponseCodes.ErrorTryAgain => StatusCode(StatusCodes.Status500InternalServerError),
        ResponseCodes.ServiceUnavailable => StatusCode(StatusCodes.Status503ServiceUnavailable),
        _ => BadRequest(response),
    };
}
