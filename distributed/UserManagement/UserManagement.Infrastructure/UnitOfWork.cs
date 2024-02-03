using UserManagement.Domain.Core.Interfaces;
using UserManagement.Domain.Repositories;

namespace UserManagement.Infrastructure;

internal sealed class UnitOfWork(
    UserManagementContext context,
    IUserRepository userRepository)
    : IUnitOfWork
{
    public IUserRepository UserRepository { get; } = userRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}