using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.Factories.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain;
using MediatR;

namespace Hospital.Integration.Application.QueriesHandlers.Common;

public class ProfessionalsQuery : IRequest<ListResponse>
{
    public string? Id { get; init; }

    public string? Name { get; init; }

    public string? Cpf { get; init; }

    public string? Email { get; init; }

    public string? PhoneNumber1 { get; init; }

    public string? PhoneNumber2 { get; init; }

    public string? PhoneNumber3 { get; init; }

    public bool? Active { get; init; }

    public FilterPaging? FilterPaging { get; init; }

    public Request Request { get; init; }
}

public class ProfessionalsQueryHandler : IRequestHandler<ProfessionalsQuery, ListResponse>
{
    private readonly IProfessionalRepository _professionalRepository;

    public ProfessionalsQueryHandler(
        IProfessionalRepository professionalRepository) =>
        _professionalRepository = professionalRepository ?? throw new ArgumentNullException(nameof(professionalRepository));

    public async Task<ListResponse> Handle(ProfessionalsQuery professionalsQuery, CancellationToken cancellationToken)
    {
        var totalCount = await _professionalRepository.GetByParamnsCountAsync(professionalsQuery);

        var professionals = await _professionalRepository.GetByParamnsAsync(professionalsQuery);

        return ProfessionalFactory.ToGet(professionalsQuery.Request, totalCount, professionals);
    }

}
