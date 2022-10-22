using Hospital.Integration.Api.Factory.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Integration.Api.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class AccommodationCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccommodationCategoriesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AccommodationCategoryByParamAsync(string request)
    {
        var accommodationCategoriesQuery = AccommodationCategoryFactory.FromGet(request);

        var accommodationCategories = await _mediator.Send(accommodationCategoriesQuery);
        return await Task.FromResult(Ok(accommodationCategories));
    }
}
