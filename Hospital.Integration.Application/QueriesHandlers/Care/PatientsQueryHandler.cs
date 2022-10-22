using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.Factories.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain;
using MediatR;

namespace Hospital.Integration.Application.QueriesHandlers.Common;

public class PatientsQuery : IRequest<ListResponse>
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

public class PatientsQueryHandler : IRequestHandler<PatientsQuery, ListResponse>
{
    private readonly IPatientRepository _patientRepository;

    public PatientsQueryHandler(
        IPatientRepository patientRepository) =>
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));

    public async Task<ListResponse> Handle(PatientsQuery patientsQuery, CancellationToken cancellationToken)
    {
        var totalCount = await _patientRepository.GetByParamnsCountAsync(patientsQuery);

        var patients = await _patientRepository.GetByParamnsAsync(patientsQuery);

        return PatientFactory.ToGet(patientsQuery.Request, totalCount, patients);
    }
}
