using Dapper;
using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain;
using Hospital.Integration.Domain.Commons;
using Hospital.Integration.Infra.Data.Repositories.Shared.Statements;
using System.Data;
using System.Text;

namespace Hospital.Integration.Infra.Data.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private const int MaxTimeOut = 60;
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentRepository(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<string> CreateAsync(Department department)
    {
        await _unitOfWork.Connection.ExecuteAsync(
            sql: DepartmentStmt.Create,
            param: new
            {
                Id = department.Id,
                Name = department.Name,
                Active = department.Active,
            },
            transaction: _unitOfWork.Transaction,
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text);

        return department.Id ?? string.Empty;
    }

    public async Task<Department> GetByIdAsync(string id) =>
     await _unitOfWork.Connection.QuerySingleAsync<Department>(
           sql: DepartmentStmt.SelectById,
           param: new
           {
               Id = id,
           },
           transaction: _unitOfWork.Transaction,
           commandTimeout: MaxTimeOut,
           commandType: CommandType.Text);

    public async Task<IEnumerable<Department>> GetByParamnsAsync(DepartmentsQuery departmentquery)
    {
        var (query, paramFilter) = MountFilter(DepartmentStmt.SelectByParamns, departmentquery, departmentquery.FilterPaging);

        return await _unitOfWork.Connection.QueryAsync<Department>(
            sql: query,
            param: paramFilter,
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text);
    }

    public async Task<int> GetByParamnsCountAsync(DepartmentsQuery departmentquery)
    {
        var (query, paramFilter) = MountFilter(DepartmentStmt.SelectByParamnsCount, departmentquery);
        return await _unitOfWork.Connection.QuerySingleAsync<int>(
            sql: query,
            param: paramFilter,
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text);
    }

    public Task<int> UpdateAsync(Department department)
    {
        throw new NotImplementedException();
    }

    private static (string, DynamicParameters) MountFilter(string select, DepartmentsQuery filter, FilterPaging? filterPaging = null)
    {
        var query = new StringBuilder(select);

        var queryFilter = new StringBuilder(" Where 1 = 1 ");

        var paramFilter = new DynamicParameters();
        if (filter is not null)
        {
            if (!string.IsNullOrEmpty(filter.Id))
            {
                queryFilter.Append(" And [Department].[Id] = @Id");
                paramFilter.Add("@Id", dbType: DbType.String, value: filter.Id, direction: ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                queryFilter.Append(" And Contains([Department].[Name],  @Name)");
                paramFilter.Add("@Name", dbType: DbType.String, value: $"\"{filter.Name}*\"", direction: ParameterDirection.Input);
            }
        }

        query.Append(queryFilter);

        if (filterPaging != null)
        {
            if (filterPaging.Skip.HasValue && filterPaging.Take.HasValue)
            {
                query.Append(" ORDER BY ").Append(filterPaging.Sort).Append(" OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;");
                paramFilter.Add("@skip", dbType: DbType.Int32, value: filterPaging.Skip, direction: ParameterDirection.Input);
                paramFilter.Add("@take", dbType: DbType.Int32, value: filterPaging.Take, direction: ParameterDirection.Input);
            }
        }

        return (query.ToString(), paramFilter);
    }
}
