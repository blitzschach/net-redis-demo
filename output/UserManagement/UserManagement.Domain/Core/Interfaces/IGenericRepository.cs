namespace UserManagement.Domain.Core.Interfaces;

public interface IGenericRepository<T>
    where T : class
{
    T Add(T entity);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Remove(T entity);
}