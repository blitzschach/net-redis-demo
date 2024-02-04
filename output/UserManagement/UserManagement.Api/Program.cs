using UserManagement.Api.Endpoints;
using UserManagement.Application;
using UserManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var postgresConn = builder.Configuration.GetConnectionString("Postgres")!;

builder.Services
    .AddApplication()
    .AddInfrastructure(postgresConn);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

UserEndpoints.Map(app);

app.Run();