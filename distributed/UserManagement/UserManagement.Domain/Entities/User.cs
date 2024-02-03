namespace UserManagement.Domain.Entities;

public sealed record User
{
    public required Guid Id { get; init; }
    
    public required string Firstname { get; init; } = string.Empty;

    public required string Lastname { get; init; } = string.Empty;
}