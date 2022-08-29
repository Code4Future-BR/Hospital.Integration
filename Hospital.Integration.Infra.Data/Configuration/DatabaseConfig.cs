using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Hospital.Integration.Infra.Data.Configuration;

[ExcludeFromCodeCoverage]
public record DatabaseConfig()
{
    [Required(AllowEmptyStrings = false)]
    public string? ConnectionString { get; init; }
}