using Hospital.Integration.Api.Factories;
using Hospital.Integration.Application.QueriesHandlers.Common;
using System.Text.Json;

namespace Hospital.Integration.Api.Factory.Common;

public static class AccommodationCategoryFactory
{
    public static AccommodationCategoriesQuery? FromGet(string request)
    {
        var requestModel = RequestFactory.From(request);
        var filtro = JsonSerializer.Deserialize<AccommodationCategoriesQuery>(requestModel.ModelBase64 ?? string.Empty);

        return new()
        {
            Id = filtro?.Id ?? string.Empty,
            Name = filtro?.Name ?? string.Empty,
            Active = filtro?.Active ?? null,
            FilterPaging = new()
            {
                Skip = requestModel.Skip,
                Sort = requestModel.Sort,
                Take = requestModel.Take,
            },
            Request = requestModel,
        };
    }
}
