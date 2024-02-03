using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Core.Interfaces;

namespace UserManagement.Infrastructure.Repositories;

internal class GenericRepository<T>
    : IGenericRepository<T>
    where T : class
{
    private readonly UserManagementContext _dbContext;

    protected GenericRepository(UserManagementContext context)
        => _dbContext = context;

    public T Add(T entity)
        => _dbContext.Set<T>().Add(entity).Entity;

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().FindAsync(id, cancellationToken);

    public void Remove(T entity)
        => _dbContext.Set<T>().Remove(entity);
}