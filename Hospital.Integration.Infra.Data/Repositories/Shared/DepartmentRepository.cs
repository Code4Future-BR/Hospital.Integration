using Hospital.Integration.Business.Abstractions.Data;
using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Infra.Data.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentRepository(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public Task<int> CreateAsync(Department department)
    {
        throw new NotImplementedException();
    }

    public Task<Department> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Department>> GetByParamAsync(Department? department, FilterPaging? filterPaging)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetByParamCountAsync(Department? department)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Department department)
    {
        throw new NotImplementedException();
    }
}
