namespace Hospital.Integration.Business.Models;

public record Request
{
    public string? Id { get; init; }

    public string? Type { get; init; }

    public string? Sort { get; init; }

    public int? Skip { get; init; }

    public int? Take { get; init; }

    public DateTimeOffset? DateNow { get; init; }

    public string? ModelBase64 { get; init; }

}
