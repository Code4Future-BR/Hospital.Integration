using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Domain.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital.Integration.Application.QueriesHandlers.Security;

public class UserValidateCredentialQuery : IRequest<UserValidateCredentialQueryResult?>
{
    public string Email { get; init; } = string.Empty;

    public string PasswordHash { get; init; } = string.Empty;
}

public class UserValidateCredentialQueryResult
{
    public string? AccessToken { get; init; }

    public string? TokenType { get; init; }

    public string? ExpiresIn { get; init; }
}

public class UserValidateCredentialQueryHandler : IRequestHandler<UserValidateCredentialQuery, UserValidateCredentialQueryResult?>
{
    private const string ClaimType = "JWT";
    private const string TokenType = "bearer";
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public UserValidateCredentialQueryHandler(
        IConfiguration configuttion,
        IUserRepository userRepository)
    {
        _configuration = configuttion ?? throw new ArgumentNullException(nameof(configuttion));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UserValidateCredentialQueryResult?> Handle(UserValidateCredentialQuery userValidateCredentialQuery, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ValidateCredentialAsync(userValidateCredentialQuery.Email, userValidateCredentialQuery.PasswordHash);
        if (!userExists)
        {
            return null;
        }

        var user = await _userRepository.GetByEmailAsync(userValidateCredentialQuery.Email);
        return GenerateJwt(user);
    }

    private UserValidateCredentialQueryResult GenerateJwt(User user)
    {
        var secretKey = _configuration["Jwt:SecretKey"];
        var expiresTime = _configuration["Jwt:ExpiresTime"];
        var issuer = _configuration["Jwt:ValidIssuer"];
        var audience = _configuration["Jwt:ValidAudience"];
        var issuedAt = DateTime.Now;
        var expires = issuedAt.AddMinutes(Convert.ToDouble(expiresTime));
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Typ, ClaimType),
                new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}" ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            },
            issuer: issuer,
            audience: audience,
            expires: expires,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.WriteToken(token);

        return new UserValidateCredentialQueryResult
        {
            AccessToken = jwtToken,
            TokenType = TokenType,
            ExpiresIn = expires.Subtract(issuedAt).TotalSeconds.ToString(CultureInfo.InvariantCulture),
        };
    }
}
