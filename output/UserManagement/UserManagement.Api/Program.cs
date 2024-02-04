using UserManagement.Api.Endpoints;
using UserManagement.Application;
using UserManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var postgresConn = builder.Configuration.GetConnectionString("Postgres")!;
var redisConn = builder.Configuration.GetConnectionString("Redis")!;

builder.Services
    .AddApplication()
    .AddInfrastructure(postgresConn);

builder.Services
    .AddOutputCache()
    .AddStackExchangeRedisOutputCache(o =>
    {
        o.InstanceName = "UserManagement";
        o.Configuration = redisConn;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOutputCache();

UserEndpoints.Map(app);

app.Run();