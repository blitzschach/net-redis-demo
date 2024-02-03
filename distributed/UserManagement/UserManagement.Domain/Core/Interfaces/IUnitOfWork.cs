using UserManagement.Domain.Repositories;

namespace UserManagement.Domain.Core.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}