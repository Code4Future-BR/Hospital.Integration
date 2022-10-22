using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain.Commons;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IProfessionalRepository
{
    Task<int> GetByParamnsCountAsync(ProfessionalsQuery professionalQuery);

    Task<IEnumerable<Professional>> GetByParamnsAsync(ProfessionalsQuery professionalQuery);
}
