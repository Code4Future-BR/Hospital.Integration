using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.Factories.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain;
using MediatR;

namespace Hospital.Integration.Application.QueriesHandlers.Common;

public class DepartmentsQuery : IRequest<ListResponse>
{
    public string? Id { get; init; }

    public string? Name { get; init; }

    public bool? Active { get; init; }

    public FilterPaging? FilterPaging { get; init; }

    public Request Request { get; init; }
}

public class DepartmentsQueryHandler : IRequestHandler<DepartmentsQuery, ListResponse>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentsQueryHandler(
        IDepartmentRepository departmentRepository) =>
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));

    public async Task<ListResponse> Handle(DepartmentsQuery departmentsQuery, CancellationToken cancellationToken)
    {
        var totalCount = await _departmentRepository.GetByParamnsCountAsync(departmentsQuery);

        var departments = await _departmentRepository.GetByParamnsAsync(departmentsQuery);

        return DepartmentFactory.ToGet(departmentsQuery.Request, totalCount, departments);
    }
}
