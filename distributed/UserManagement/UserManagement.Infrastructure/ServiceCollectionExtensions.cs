using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domain.Core.Interfaces;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        string postgresConn,
        string redisConn)
    {
        services
            .AddDbContext<UserManagementContext>(o =>
            {
                o.UseNpgsql(postgresConn);
            })
            .AddStackExchangeRedisCache(o =>
            {
                o.Configuration = redisConn;
            });

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}