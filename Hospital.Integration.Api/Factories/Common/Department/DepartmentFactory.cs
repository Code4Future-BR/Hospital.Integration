using Hospital.Integration.Api.Factories;
using Hospital.Integration.Application.Handlers.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain.Commons;
using System.Text.Json;

namespace Hospital.Integration.Api.Factory.Common;

public static class DepartmentFactory
{
    public static DepartmentCreateCommand? FromCreate(string request)
    {
        var requestModel = RequestFactory.From(request);
        return JsonSerializer.Deserialize<DepartmentCreateCommand>(requestModel.ModelBase64 ?? string.Empty);
    }

    public static DepartmentCreateCommand? FromUpdate(Request request)
    {
        if (string.IsNullOrEmpty(request.ModelBase64) || request.ModelBase64 == "{}")
        {
            return null;
        }

        var model = JsonSerializer.Deserialize<Department>(request.ModelBase64);

        if (model is null)
        {
            return null;
        }

        return
            new()
            {
                Name = model.Name,
                Active = model.Active,
            };
    }

    public static DepartmentsQuery? FromGet(string request)
    {
        var requestModel = RequestFactory.From(request);
        var filtro = JsonSerializer.Deserialize<DepartmentsQuery>(requestModel.ModelBase64 ?? string.Empty);

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
