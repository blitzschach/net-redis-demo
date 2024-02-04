using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Infrastructure.Repositories;

internal sealed class UserRepository(UserManagementContext context)
    : GenericRepository<User>(context), IUserRepository;