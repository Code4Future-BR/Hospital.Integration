using Hospital.Integration.Api.Factories;
using Hospital.Integration.Api.Factory.Common;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DepartmentsByParamAsync(string request)
    {
        var departmentsQuery = DepartmentFactory.FromGet(request);

        var departments = await _mediator.Send(departmentsQuery);
        return await Task.FromResult(Ok(departments));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DepartmentCreateAsync(string request)
    {
        var departmentCreateCommand = DepartmentFactory.FromCreate(request);

        if (departmentCreateCommand is null)
        {
            return await Task.FromResult(BadRequest("Invalid data"));
        }

        var id = await _mediator.Send(departmentCreateCommand);
        return await Task.FromResult(Created(string.Empty, id));
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
        return await Task.FromResult(Created(string.Empty, id));
    }
}
