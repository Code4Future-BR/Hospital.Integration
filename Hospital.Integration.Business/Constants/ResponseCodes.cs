using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Hospital.Integration.Business.Constants;

[ExcludeFromCodeCoverage]
public static class ResponseCodes
{
    public const int ErrorTryAgain = 06;
    public const int TechnicalFailure = 07;
    public const int InvalidData = 30;
    public const int InconsistentData = 40;
    public const int Nonexistent = 46;
    public const int ServiceUnavailable = 96;
    public const int Unauthorized = 41;
    public const int Success = 200;
    public const int Created = 201;
    public const int NotApplicable = -1;

    public static string GetDescriptorTo(int responseCode) => responseCode switch
    {
        ErrorTryAgain => "Error Try Again",
        Created => "Created with Success",
        TechnicalFailure => "Technical Failure.",
        InvalidData => "Invalid data.",
        InconsistentData => "Inconsistent data.",
        Nonexistent => "Invalid Code",
        ServiceUnavailable => "Service Unavailable",
        Unauthorized => "Unauthorized User",
        _ => responseCode.ToString(),
    };

    public static HttpStatusCode GetHttpStatusCodeTo(int responseCode) => responseCode switch
    {
        ErrorTryAgain => HttpStatusCode.InternalServerError,
        Created => HttpStatusCode.Created,
        TechnicalFailure => HttpStatusCode.InternalServerError,
        InvalidData => HttpStatusCode.BadRequest,
        Nonexistent => HttpStatusCode.NotFound,
        ServiceUnavailable => HttpStatusCode.ServiceUnavailable,
        Unauthorized => HttpStatusCode.Unauthorized,
        _ => HttpStatusCode.BadRequest,
    };
}
