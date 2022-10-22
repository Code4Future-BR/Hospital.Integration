using Hospital.Integration.Application.Constants;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain.Care;
using System.Text.Json;

namespace Hospital.Integration.Application.Factories.Common;

public static class PatientFactory
{
    public static ListResponse ToGet(Request request, int totalCount, IEnumerable<Patient> patients) =>
        new()
        {
            Id = request.Id,
            ResponseDate = request.DateNow,
            ResponseCode = ResponseCodes.Success,
            Items = JsonSerializer.Serialize(patients),
            Sort = request.Sort,
            Skip = request.Skip,
            Take = request.Take,
            TotalCount = totalCount,
            TotalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / Convert.ToInt32(request.Take))),
        };
}
