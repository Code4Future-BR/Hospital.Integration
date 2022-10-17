using Dapper;
using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Domain.Security;
using System.Data;

namespace Hospital.Integration.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private const int MaxTimeOut = 60;
    private const int IsValid = 1;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<User> GetByEmailAsync(string email) =>
       await _unitOfWork.Connection.QuerySingleAsync<User>(
            sql: UserStmt.SelectByEmail,
            param: new
            {
                Email = email,
            },
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text);

    public async Task<bool> ValidateCredentialAsync(string email, string password) =>
        await _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(
            sql: UserStmt.SelectValidateCredential,
            param: new
            {
                Email = email,
                Password = password,
            },
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text) == IsValid;
}
