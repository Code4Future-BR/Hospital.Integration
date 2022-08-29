using Hospital.Integration.Business.Entities;

namespace Hospital.Integration.Business.Abstractions.Authentication;

public interface IUserRepository
{
    Task<bool> ValidateCredentialsAsync(string email, string password);

    Task<User> GetByEmailAsync(string email);
}
