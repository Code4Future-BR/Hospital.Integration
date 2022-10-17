using Hospital.Integration.Business.Constants;
using Hospital.Integration.Business.Entities.Common;
using System.Text.Json;

namespace Hospital.Integration.Business.Models;

public static class DepartmentResponse
{
    public static BaseResponse FromByParam(Request request, int totalCount, IEnumerable<Department> departments)
    {
        var departmentsModel = new List<Department>();

        foreach (var department in departments)
        {
            departmentsModel.Add(new()
            {
                Id = department.Id,
                Name = department.Name,
                Active = department.Active,
            });
        }

        return new()
        {
            Id = request.Id ?? string.Empty,
            ResponseDate = request.DateNow,
            ResponseCode = ResponseCodes.Success,
            Items = JsonSerializer.Serialize(departmentsModel),
            Sort = request.Sort,
            Skip = request.Skip,
            Take = request.Take,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / (int)request.Take),
        };
    }
}
