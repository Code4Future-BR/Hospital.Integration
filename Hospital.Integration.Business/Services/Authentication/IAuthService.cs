using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Business.Services;

public interface IAuthService
{
    Task<AuthResponse> GenerateToken(string email, string password);
}
