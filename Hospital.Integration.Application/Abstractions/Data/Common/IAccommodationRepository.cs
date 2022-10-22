using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain.Commons;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IAccommodationRepository
{
    Task<int> GetByParamnsCountAsync(AccommodationsQuery accommodations);

    Task<IEnumerable<Accommodation>> GetByParamnsAsync(AccommodationsQuery accommodations);
}
