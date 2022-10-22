using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.Factories.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain;
using MediatR;

namespace Hospital.Integration.Application.QueriesHandlers.Common;

public class AccommodationCategoriesQuery : IRequest<ListResponse>
{
    public string? Id { get; init; }

    public string? Name { get; init; }

    public bool? Active { get; init; }

    public FilterPaging? FilterPaging { get; init; }

    public Request Request { get; init; }
}

public class AccommodationCategoriesQueryHandler : IRequestHandler<AccommodationCategoriesQuery, ListResponse>
{
    private readonly IAccommodationCategoryRepository _accommodationCategoryRepository;

    public AccommodationCategoriesQueryHandler(
        IAccommodationCategoryRepository accommodationCategoryRepository) =>
        _accommodationCategoryRepository = accommodationCategoryRepository ?? throw new ArgumentNullException(nameof(accommodationCategoryRepository));

    public async Task<ListResponse> Handle(AccommodationCategoriesQuery accommodationCategoriesQuery, CancellationToken cancellationToken)
    {
        var totalCount = await _accommodationCategoryRepository.GetByParamnsCountAsync(accommodationCategoriesQuery);

        var accommodationCategories = await _accommodationCategoryRepository.GetByParamnsAsync(accommodationCategoriesQuery);

        return AccommodationCategoryFactory.ToGet(accommodationCategoriesQuery.Request, totalCount, accommodationCategories);
    }
}
