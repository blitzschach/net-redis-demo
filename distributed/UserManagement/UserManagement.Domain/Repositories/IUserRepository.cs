using UserManagement.Domain.Core.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IUserRepository
    : IGenericRepository<User>
{
}