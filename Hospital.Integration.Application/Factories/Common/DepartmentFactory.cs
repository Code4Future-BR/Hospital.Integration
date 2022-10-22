using Hospital.Integration.Application.Constants;
using Hospital.Integration.Application.Handlers.Common;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain.Commons;
using System.Text.Json;

namespace Hospital.Integration.Application.Factories.Common;

public static class DepartmentFactory
{
    public static ListResponse ToGet(Request request, int totalCount, IEnumerable<Department> departments) =>
        new()
        {
            Id = request.Id,
            ResponseDate = request.DateNow,
            ResponseCode = ResponseCodes.Success,
            Items = JsonSerializer.Serialize(departments),
            Sort = request.Sort,
            Skip = request.Skip,
            Take = request.Take,
            TotalCount = totalCount,
            TotalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / Convert.ToInt32(request.Take))),
        };

    public static Department FromCreate(DepartmentCreateCommand createDepartmentCommand) =>
        new Department(createDepartmentCommand.Id, createDepartmentCommand.Name, createDepartmentCommand.Active);

    public static Department FromUpdate(DepartmentUpdateCommand updateDepartmentCommand) =>
         new Department(updateDepartmentCommand.Id, updateDepartmentCommand.Name, updateDepartmentCommand.Active);
}
