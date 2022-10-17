using Hospital.Integration.Application.Handlers.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Domain.Commons;
using System.Text.Json;

namespace Hospital.Integration.Api.Factory.Common;

public static class DepartmentFactory
{
    public static DepartmentUpdateCommand? FromCreate(Request request)
    {
        if (string.IsNullOrEmpty(request.ModelBase64) || request.ModelBase64 == "{}")
        {
            return null;
        }

        var model = JsonSerializer.Deserialize<DepartmentUpdateCommand>(request.ModelBase64);

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

    public static DepartmentsQuery FromGet(Request request)
    {
        if (string.IsNullOrEmpty(request.ModelBase64) || request.ModelBase64 == "{}")
        {
            return
                new()
                {
                    Id = string.Empty,
                    Name = string.Empty,
                    Active = false,
                };
        }

        var model = JsonSerializer.Deserialize<Department>(request.ModelBase64);

        return
            new()
            {
                Id = model?.Id,
                Name = model?.Name,
                Active = model.Active,
            };
    }
}
