using Hospital.Integration.Business.Abstractions.Authentication;
using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital.Integration.Business.Services;

public class AuthService : IAuthService
{
    private const string TokenType = "bearer";
    private const string ClaimType = "JWT";
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<AuthResponse> GenerateToken(
        string email, string password)
    {
        var isUser = await ValidateCredentialsAsync(email, password);

        if (!isUser)
        {
            return AuthResponse.From();
        }

        var user = await _userRepository.GetByEmailAsync(email);

        return AuthResponse.From(GenerateJwt(user));
    }

    private async Task<bool> ValidateCredentialsAsync(string email, string password) =>
        await _userRepository.ValidateCredentialsAsync(email, password);

    private Token GenerateJwt(User user)
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

        return new Token
        {
            AccessToken = jwtToken,
            TokenType = TokenType,
            ExpiresIn = expires.Subtract(issuedAt).TotalSeconds.ToString(CultureInfo.InvariantCulture),
        };
    }
}
