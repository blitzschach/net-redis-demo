using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Services;
using UserManagement.Application.Services.Interfaces;

namespace UserManagement.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}