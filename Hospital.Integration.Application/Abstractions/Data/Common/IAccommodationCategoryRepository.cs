using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain.Commons;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IAccommodationCategoryRepository
{
    Task<int> GetByParamnsCountAsync(AccommodationCategoriesQuery accommodationCategoriesQuery);

    Task<IEnumerable<AccommodationCategory>> GetByParamnsAsync(AccommodationCategoriesQuery accommodationCategoriesQuery);
}
