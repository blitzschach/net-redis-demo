using UserManagement.Application.Models;
using UserManagement.Application.Services.Interfaces;
using UserManagement.Domain.Core.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services;

internal sealed class UserService(IUnitOfWork unitOfWork) 
    : IUserService
{
    public async Task<UserDto?> CreateAsync(
        CreateUserDto user,
        CancellationToken cancellationToken = default)
    {
        var addedUser =
            unitOfWork
                .UserRepository
                .Add(new User
                {
                    Firstname = user.Firstname,
                    Id = Guid.NewGuid(),
                    Lastname = user.Lastname,
                });

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return addedUser;
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var user =
            await unitOfWork
                .UserRepository
                .GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return;
        }
        
        unitOfWork.UserRepository.Remove(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserDto?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        => await unitOfWork
            .UserRepository
            .GetByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<UserDto?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users =
            await unitOfWork
                .UserRepository
                .GetAllAsync(cancellationToken);

        return users.Select(u => (UserDto?)u);
    }
}