using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure;

public sealed class UserManagementContext
    : DbContext
{
    public UserManagementContext(DbContextOptions<UserManagementContext> options)
        : base(options)
    {
    }
    
    public DbSet<User>? Users { get; set; }
}