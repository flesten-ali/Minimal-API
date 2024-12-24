using Microsoft.EntityFrameworkCore;
using MinimalAPI.Configuration;
using MinimalAPI.DbContexts;
using MinimalAPI.Extensions;
using MinimalAPI.Interfaces;
using MinimalAPI.Services;
using MinimalAPI.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Extension method for adding  JWT Configuration
builder.Services.AddJwtAuthentication(builder.Configuration);

//Dependency Injection
builder.Services.AddDbContext<MinimalDbContext>(opt => opt.UseInMemoryDatabase("MinimalDatabase"));
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IJWTTokenServices, JWTTokenGenerator>();

//Register JWTConfiguration in the DI container and bind it to the JWTToken section in appsettings.json
builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection("JWTToken"));

//Extension method for adding Swagger configuration
builder.Services.AddCustomSwagger();

var app = builder.Build();

//automatically seeds users in the -> IN MEMORY DB
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MinimalDbContext>();
    UserSeeder.SeedUsers(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();