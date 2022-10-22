﻿using Hospital.Integration.Application.Constants;
using Hospital.Integration.Application.Models;
using Hospital.Integration.Domain.Commons;
using System.Text.Json;

namespace Hospital.Integration.Application.Factories.Common;

public static class AccommodationFactory
{
    public static ListResponse ToGet(Request request, int totalCount, IEnumerable<Accommodation> accommodations) =>
        new()
        {
            Id = request.Id,
            ResponseDate = request.DateNow,
            ResponseCode = ResponseCodes.Success,
            Items = JsonSerializer.Serialize(accommodations),
            Sort = request.Sort ?? string.Empty,
            Skip = request.Skip,
            Take = request.Take ?? 0,
            TotalCount = totalCount,
            TotalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / Convert.ToInt32(request.Take ?? 0))),
        };
}