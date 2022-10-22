using Hospital.Integration.Api.Factories;
using Hospital.Integration.Application.QueriesHandlers.Common;
using System.Text.Json;

namespace Hospital.Integration.Api.Factory.Common;

public static class PatientFactory
{
    public static PatientsQuery? FromGet(string request)
    {
        var requestModel = RequestFactory.From(request);
        var filtro = JsonSerializer.Deserialize<PatientsQuery>(requestModel.ModelBase64 ?? string.Empty);

        return new()
        {
            Id = filtro?.Id ?? string.Empty,
            Name = filtro?.Name ?? string.Empty,
            Cpf = filtro?.Cpf ?? string.Empty,
            Email = filtro?.Email ?? string.Empty,
            PhoneNumber1 = filtro?.PhoneNumber1 ?? string.Empty,
            PhoneNumber2 = filtro?.PhoneNumber2 ?? string.Empty,
            PhoneNumber3 = filtro?.PhoneNumber3 ?? string.Empty,
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
