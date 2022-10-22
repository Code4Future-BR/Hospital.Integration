
using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain.Commons;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IDepartmentRepository
{
    Task<Department> GetByIdAsync(string id);

    Task<int> GetByParamnsCountAsync(DepartmentsQuery departmentQuery);

    Task<IEnumerable<Department>> GetByParamnsAsync(DepartmentsQuery departmentQuery);

    Task<string> CreateAsync(Department department);

    Task<int> UpdateAsync(Department department);
}
