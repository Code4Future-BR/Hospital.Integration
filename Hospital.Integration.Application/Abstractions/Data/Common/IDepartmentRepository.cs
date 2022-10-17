
using Hospital.Integration.Domain.Commons;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IDepartmentRepository
{
    Task<Department> GetByIdAsync(string id);

    Task<int> GetByParamnsCountAsync(Department department);

    Task<IEnumerable<Department>> GetByParamnsAsync(Department department, FilterPaging filterPaging);

    Task<string> CreateAsync(Department department);

    Task<int> UpdateAsync(Department department);
}
