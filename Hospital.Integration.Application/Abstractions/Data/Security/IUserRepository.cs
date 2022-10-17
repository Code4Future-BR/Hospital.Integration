using Hospital.Integration.Domain.Security;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IUserRepository
{
    Task<bool> ValidateCredentialAsync(string email, string password);

    Task<User> GetByEmailAsync(string email);
}
