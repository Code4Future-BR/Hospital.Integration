using Hospital.Integration.Business.Abstractions.Data;
using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Factories;
using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Business.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestFactory _requestFactory;
    private readonly IDepartmentFactory _departmentFactory;
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(
        IUnitOfWork uow,
        IRequestFactory requestFactory,
        IDepartmentFactory departmentFactory,
        IDepartmentRepository departmentRepository)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
        _departmentFactory = departmentFactory ?? throw new ArgumentNullException(nameof(departmentFactory));
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    }

    public async Task<(int, string)> CreateAsync(string request)
    {
        _uow.BeginTransaction();
        var requestModel = _requestFactory.From(request);
        var department = _departmentFactory.FromCreate(requestModel, string.Empty);
        var ret = await _departmentRepository.CreateAsync(department);
        _uow.Commit();

        return (ret, department?.Id ?? string.Empty);
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
