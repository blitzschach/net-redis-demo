using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UserManagement.Api.Routes;
using UserManagement.Application.Models;
using UserManagement.Application.Services.Interfaces;

namespace UserManagement.Api.Endpoints;

internal static class UserEndpoints
{
    private const string BaseTag = "Users";
    
    public static void Map(WebApplication app)
    {
        app.MapPost(UserRoutes.Create, CreateUser)
            .Produces<UserDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags(BaseTag);

        app.MapDelete(UserRoutes.Delete, DeleteUser)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(BaseTag);

        app.MapGet(UserRoutes.Get, GetUser)
            .CacheOutput(o => o.Expire(TimeSpan.FromMinutes(2)).Tag("get_user"))
            .Produces<UserDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(BaseTag);

        app.MapGet(UserRoutes.GetAll, GetAllUsers)
            .CacheOutput(o => o.Expire(TimeSpan.FromMinutes(2)).Tag("get_users"))
            .Produces<IEnumerable<UserDto>>()
            .WithTags(BaseTag);
    }

    private static async Task<IResult> CreateUser(
        [FromBody] CreateUserDto user,
        IUserService userService,
        IOutputCacheStore cache,
        CancellationToken cancellationToken)
    {
        var createdUser =
            await userService.CreateAsync(user, cancellationToken);

        await cache.EvictByTagAsync("get_users", cancellationToken);

        return
            createdUser is null
                ? Results.BadRequest("Failed to create user.")
                : Results.Created(
                    UserRoutes.Get
                        .Replace(
                            "{id:guid}",
                            createdUser.Id.ToString("D")),
                    createdUser);
    }

    private static async Task<IResult> DeleteUser(
        [FromRoute] Guid id,
        IUserService userService,
        IOutputCacheStore cache,
        CancellationToken cancellationToken)
    {
        await userService.DeleteAsync(id, cancellationToken);
        await cache.EvictByTagAsync("get_user", cancellationToken);
        await cache.EvictByTagAsync("get_users", cancellationToken);
        
        return Results.NoContent();
    }

    private static async Task<IResult> GetUser(
        [FromRoute] Guid id,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var user =
            await userService.GetAsync(id, cancellationToken);

        return
            user is null
                ? Results.NotFound()
                : Results.Ok(user);
    }

    private static async Task<IResult> GetAllUsers(
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var users =
            await userService.GetAllAsync(cancellationToken);

        return Results.Ok(users);
    }
}