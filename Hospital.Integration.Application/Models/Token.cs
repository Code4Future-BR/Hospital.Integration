namespace Hospital.Integration.Application.Models;

public record Token
{
    public string? AccessToken { get; init; }
    public string? TokenType { get; init; }
    public string? ExpiresIn { get; init; }
    public string? UserId { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
}

