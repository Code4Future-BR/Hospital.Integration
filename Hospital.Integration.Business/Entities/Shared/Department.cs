namespace Hospital.Integration.Business.Entities;

public record Department
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public bool? Active { get; init; }
}
