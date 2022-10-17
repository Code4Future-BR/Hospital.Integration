using Hospital.Integration.Business.Entities.Common;
using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Business.Abstractions.Data;

public interface IDepartmentRepository
{
    Task<int> GetByParamCountAsync(Department? department);

    Task<Department> GetByIdAsync(string id);

    Task<IEnumerable<Department>> GetByParamAsync(Department? department, FilterPaging? filterPaging);

    Task<int> CreateAsync(Department department);

    Task<int> UpdateAsync(Department department);
}
