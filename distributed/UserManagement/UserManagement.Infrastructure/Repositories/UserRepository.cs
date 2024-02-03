using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Infrastructure.Repositories;

internal sealed class UserRepository()
    : IUserRepository
{
    private readonly UserManagementContext _dbContext;

    public UserRepository(UserManagementContext context) 
        : this()
        => _dbContext = context;

    public User Add(User user)
        => _dbContext
            .Set<User>()
            .Add(user).Entity;

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext
            .Set<User>()
            .ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public void Remove(User user)
        => _dbContext
            .Set<User>()
            .Remove(user);
}