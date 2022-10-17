using System.Diagnostics.CodeAnalysis;

namespace Hospital.Integration.Api.Constants;

[ExcludeFromCodeCoverage]
public static class ResponseCodes
{
    public const int Unauthorized = 301;
    public const int Success = 200;
    public const int Created = 201;
    public const int NotFound = 400;
}
