using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Infrastructure.Repositories.Cached;

internal sealed class CachedUserRepository(
    UserRepository userRepository,
    IDistributedCache distributedCache,
    UserManagementContext context) 
    : IUserRepository
{
    public User Add(User user)
    {
        var key = $"user-{user.Id}";
        
        distributedCache.SetString(
            key,
            JsonConvert.SerializeObject(user));
        
        return userRepository.Add(user);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await userRepository.GetAllAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var key = $"user-{id}";

        var cachedUser =
            await distributedCache.GetStringAsync(
                key,
                cancellationToken);

        User? user;

        if (string.IsNullOrEmpty(cachedUser))
        {
            user = await userRepository.GetByIdAsync(id, cancellationToken);

            if (user is null)
            {
                return user;
            }

            await distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(user),
                cancellationToken);
        }

        user = JsonConvert.DeserializeObject<User>(cachedUser!);

        if (user is not null)
        {
            context.Set<User>().Attach(user);
        }

        return user;
    }

    public void Remove(User user)
    {
        var key = $"user-{user.Id}";
        
        distributedCache.Remove(key);
        userRepository.Remove(user);
    }
}