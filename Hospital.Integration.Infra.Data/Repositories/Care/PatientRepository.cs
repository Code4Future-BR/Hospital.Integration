using Dapper;
using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain;
using Hospital.Integration.Domain.Care;
using Hospital.Integration.Infra.Data.Repositories.Shared.Statements;
using System.Data;
using System.Text;

namespace Hospital.Integration.Infra.Data.Repositories;

public class PatientRepository : IPatientRepository
{
    private const int MaxTimeOut = 60;
    private readonly IUnitOfWork _unitOfWork;

    public PatientRepository(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<int> GetByParamnsCountAsync(PatientsQuery patientquery)
    {
        var (query, paramFilter) = MountFilter(PatientStmt.SelectByParamnsCount, patientquery);
        return await _unitOfWork.Connection.QuerySingleAsync<int>(
            sql: query,
            param: paramFilter,
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text);
    }

    public async Task<IEnumerable<Patient>> GetByParamnsAsync(PatientsQuery patientquery)
    {
        var (query, paramFilter) = MountFilter(PatientStmt.SelectByParamns, patientquery, patientquery.FilterPaging);

        return await _unitOfWork.Connection.QueryAsync<Patient>(
            sql: query,
            param: paramFilter,
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text);
    }

    private static (string, DynamicParameters) MountFilter(string select, PatientsQuery filter, FilterPaging? filterPaging = null)
    {
        var query = new StringBuilder(select);

        var queryFilter = new StringBuilder(" Where 1 = 1 ");

        var paramFilter = new DynamicParameters();
        if (filter is not null)
        {
            if (!string.IsNullOrEmpty(filter.Id))
            {
                queryFilter.Append(" And [Patient].[Id] = @Id");
                paramFilter.Add("@Id", dbType: DbType.String, value: filter.Id, direction: ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                queryFilter.Append(" And Contains([Patient].[Name],  @Name)");
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
