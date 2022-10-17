namespace Hospital.Integration.Api.Models;

public class ListResponse
{
    public string? Id { get; init; }

    public DateTimeOffset? ResponseDate { get; init; }

    public int? ResponseCode { get; init; }

    public string? Sort { get; init; }

    public int? Skip { get; init; }

    public int? Take { get; init; }

    public int? TotalCount { get; init; }

    public int? TotalPages { get; init; }

    public string? Items { get; init; }
}
