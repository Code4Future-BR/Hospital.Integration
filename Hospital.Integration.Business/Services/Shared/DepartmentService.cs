using Hospital.Integration.Business.Abstractions.Data;
using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Factories;
using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Business.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentFactory _departmentFactory;
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(
        IDepartmentFactory departmentFactory,
        IDepartmentRepository departmentRepository)
    {
        _departmentFactory = departmentFactory ?? throw new ArgumentNullException(nameof(departmentFactory));
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    }

    public Task<(int, string)> CreateAsync(string request)
    {
        throw new NotImplementedException();
    }

    public Task<Department> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> GetByParamAsync(string request)
    {
        var requestModel = RequestFactory.RequestFrom(request);
        var filterPaging = RequestFactory.FilterFrom(requestModel);
        var departmentFilter = _departmentFactory.From(requestModel);

        var totalCount = await _departmentRepository.GetByParamCountAsync(departmentFilter);
        var departments = await _departmentRepository.GetByParamAsync(departmentFilter, filterPaging);

        return DepartmentResponse.FromByParam(requestModel, totalCount, departments);
    }

    public Task<int> UpdateAsync(string request)
    {
        throw new NotImplementedException();
    }
}
