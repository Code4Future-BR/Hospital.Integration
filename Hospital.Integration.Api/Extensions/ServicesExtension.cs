using Hospital.Integration.Infra.Ioc.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Hospital.Integration.Api.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.ResponseStatusCode;
        });

        services.AddCors();
        services.AddControllers();
        services.AddIoc(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<GzipCompressionProviderOptions>(gzipCompressionOptions =>
            gzipCompressionOptions.Level = CompressionLevel.Fastest);
        services.AddResponseCompression(compressionOptions =>
        {
            compressionOptions.EnableForHttps = true;
            compressionOptions.Providers.Add<GzipCompressionProvider>();
        });
        services.AddHsts(opts =>
        {
            opts.MaxAge = TimeSpan.FromDays(1);
            opts.IncludeSubDomains = true;
            opts.Preload = true;
        });

        

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new()
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            };
        });

        
    }
}
