using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Domain.Security;
using MediatR;

namespace Hospital.Integration.Application.QueriesHandlers.Security;

public class UserByEmailQuery : IRequest<User>
{
    public string Email { get; init; } = string.Empty;
}

public class UserByEmailQueryHandler : IRequestHandler<UserByEmailQuery, User>
{
    private readonly IUserRepository _userRepository;

    public UserByEmailQueryHandler(
        IUserRepository userRepository) =>
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<User> Handle(UserByEmailQuery userByEmailQuery, CancellationToken cancellationToken) =>
        await _userRepository.GetByEmailAsync(userByEmailQuery.Email);
}