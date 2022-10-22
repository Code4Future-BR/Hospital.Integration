namespace Hospital.Integration.Domain;

public record FilterPaging
{
    public string? Sort { get; init; }

    public int? Skip { get; init; }

    public int? Take { get; init; }
}
