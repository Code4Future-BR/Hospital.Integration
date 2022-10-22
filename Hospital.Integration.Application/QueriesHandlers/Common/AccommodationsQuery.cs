using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.Factories.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain;
using MediatR;

namespace Hospital.Integration.Application.QueriesHandlers.Common;

public class AccommodationsQuery : IRequest<ListResponse>
{
    public string? Id { get; init; }

    public string? Name { get; init; }

    public bool? Active { get; init; }

    public FilterPaging? FilterPaging { get; init; }

    public Request Request { get; init; }
}

public class AccommodationsQueryHandler : IRequestHandler<AccommodationsQuery, ListResponse>
{
    private readonly IAccommodationRepository _accommodationRepository;

    public AccommodationsQueryHandler(
        IAccommodationRepository accommodationRepository) =>
        _accommodationRepository = accommodationRepository ?? throw new ArgumentNullException(nameof(accommodationRepository));

    public async Task<ListResponse> Handle(AccommodationsQuery accommodationsQuery, CancellationToken cancellationToken)
    {
        var totalCount = await _accommodationRepository.GetByParamnsCountAsync(accommodationsQuery);

        var accommodations = await _accommodationRepository.GetByParamnsAsync(accommodationsQuery);

        return AccommodationFactory.ToGet(accommodationsQuery.Request, totalCount, accommodations);
    }
}
