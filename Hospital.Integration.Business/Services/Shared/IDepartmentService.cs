using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Business.Services;

public interface IDepartmentService
{
    Task<BaseResponse> GetByParamAsync(string request);

    Task<Department> GetByIdAsync(string id);

    Task<(int, string)> CreateAsync(string request);

    Task<int> UpdateAsync(string request);
}
