using Dapper;
using Hospital.Integration.Business.Abstractions.Authentication;
using Hospital.Integration.Business.Abstractions.Data;
using Hospital.Integration.Business.Entities;
using System.Data;

namespace Hospital.Integration.Infra.Data.Repositories;

internal class UserRepository : IUserRepository
{
    private const int MaxTimeOut = 60;
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

    public async Task<bool> ValidateCredentialsAsync(string email, string password) =>
        await _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(
            sql: UserStmt.SelectValidateCredentials,
            param: new
            {
                Email = email,
                Password = password,
            },
            commandTimeout: MaxTimeOut,
            commandType: CommandType.Text) == 1;
}
