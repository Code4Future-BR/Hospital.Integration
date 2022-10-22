using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain.Care;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IPatientRepository
{
    Task<int> GetByParamnsCountAsync(PatientsQuery patientQuery);

    Task<IEnumerable<Patient>> GetByParamnsAsync(PatientsQuery patientQuery);
}
