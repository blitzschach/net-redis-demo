using UserManagement.Domain.Entities;

namespace UserManagement.Application.Models;

public sealed record UserDto
{
    public required string Firstname { get; init; } = string.Empty;
    
    public required Guid Id { get; init; }

    public required string Lastname { get; init; } = string.Empty;

    public static implicit operator UserDto?(User? user)
        => user is null
            ? null
            : new UserDto
            {
                Firstname = user.Firstname,
                Id = user.Id,
                Lastname = user.Lastname,
            };
}