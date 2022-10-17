using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Domain.Commons;
using MediatR;

namespace Hospital.Integration.Application.QueriesHandlers.Common;

public class DepartmentByIdQuery : IRequest<Department>
{
    public string Id { get; init; } = string.Empty;
}

public class DepartmentByIdQueryHandler : IRequestHandler<DepartmentByIdQuery, Department>
{
    private readonly IUnitOfWork _uow;
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentByIdQueryHandler(
        IUnitOfWork uow,
        IDepartmentRepository departmentRepository)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    }

    public async Task<Department> Handle(DepartmentByIdQuery departmentByIdQuery, CancellationToken cancellationToken)
    {
        _uow.BeginTransaction();
        var department = await _departmentRepository.GetByIdAsync(departmentByIdQuery.Id);
        _uow.Commit();

        return department;
    }
}
