namespace UserManagement.Api.Routes;

internal static class UserRoutes
{
    private const string Base = "/api/Users";
    
    public const string Create = Base;

    public const string Delete = Get;

    public const string Get = $"{Base}/{{id:guid}}";

    public const string GetAll = Base;
}