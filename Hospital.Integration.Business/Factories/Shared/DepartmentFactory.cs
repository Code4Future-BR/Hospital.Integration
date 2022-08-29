using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Models;
using System.Text.Json;

namespace Hospital.Integration.Business.Factories;

public record DepartmentFactory : IDepartmentFactory
{
    public Department? From(Request request)
    {
        if (string.IsNullOrEmpty(request.ModelBase64))
        {
            return null;
        }

        if (request.ModelBase64 == "{}")
        {
            return null;
        }

        var model = JsonSerializer.Deserialize<Department>(request.ModelBase64);

        return
            new()
            {
                Id = model?.Id,
                Name = model?.Name,
                Active = model?.Active,
            };
    }

    public Department FromCreate(Request request, string userId)
    {
        throw new NotImplementedException();
    }

    public Department FromUpdate(Request request, string userId)
    {
        throw new NotImplementedException();
    }
}
