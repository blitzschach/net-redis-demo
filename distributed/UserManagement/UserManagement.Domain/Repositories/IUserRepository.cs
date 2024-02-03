using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IUserRepository
{
    User Add(User entity);
    
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    void Remove(User entity);
}