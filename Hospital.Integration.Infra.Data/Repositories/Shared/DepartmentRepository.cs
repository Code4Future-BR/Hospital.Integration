using Dapper;
using Hospital.Integration.Business.Abstractions.Data;
using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Models;
using Hospital.Integration.Infra.Data.Repositories.Shared.Statements;
using System.Data;

namespace Hospital.Integration.Infra.Data.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private const int MaxTimeOut = 60;

    public DepartmentRepository(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<int> CreateAsync(Department department) =>
        await _unitOfWork.Connection.ExecuteAsync(
            sql: DepartmentStmt.Create,
            param: new
            {
                Id = department.Id,
                Name = department.Name,
                Active = department.Active,
            },
            _unitOfWork.Transaction,
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text);

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
