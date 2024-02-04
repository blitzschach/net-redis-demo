namespace UserManagement.Application.Models;

public sealed record CreateUserDto
{
    public required string Firstname { get; init; } = string.Empty;

    public required string Lastname { get; init; } = string.Empty;
}