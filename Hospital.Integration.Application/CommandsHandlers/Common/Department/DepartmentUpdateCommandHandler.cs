using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.Constants;
using Hospital.Integration.Application.Factories.Common;
using MediatR;

namespace Hospital.Integration.Application.Handlers.Common;

public class DepartmentUpdateCommand : IRequest<int>
{
    public string? Id { get; init; }

    public string Name { get; init; }

    public bool Active { get; init; }
}

public class DepartmentUpdateCommandHandler : IRequestHandler<DepartmentUpdateCommand, int>
{
    private readonly IUnitOfWork _uow;
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentUpdateCommandHandler(
        IUnitOfWork uow,
        IDepartmentRepository departmentRepository)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    }

    public async Task<int> Handle(DepartmentUpdateCommand departmentuUpdateCommand, CancellationToken cancellationToken)
    {
        _uow.BeginTransaction();
        var department = DepartmentFactory.FromUpdate(departmentuUpdateCommand);
        var _ = await _departmentRepository.UpdateAsync(department);
        _uow.Commit();

        return ResponseCodes.NoContent;
    }
}
