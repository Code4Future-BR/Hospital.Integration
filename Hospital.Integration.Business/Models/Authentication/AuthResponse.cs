using Hospital.Integration.Business.Constants;

namespace Hospital.Integration.Business.Models;

public record AuthResponse
{
    public string? Id { get; init; }
    public DateTimeOffset ResponseDate { get; init; }
    public int ResponseCode { get; init; }
    public string? AccessToken { get; init; }

    public static AuthResponse From()
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            ResponseDate = DateTime.Now,
            ResponseCode = ResponseCodes.Unauthorized,
            AccessToken = string.Empty,
        };

    public static AuthResponse From(Token token)
    {
        var responseCode = ResponseCodes.Success;

        if (string.IsNullOrEmpty(token.AccessToken))
        {
            responseCode = ResponseCodes.Unauthorized;
        }

        return new()
        {
            Id = Guid.NewGuid().ToString(),
            ResponseDate = DateTime.Now,
            ResponseCode = responseCode,
            AccessToken = token.AccessToken,
        };
    }
}
