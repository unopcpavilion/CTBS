using CTBS.API.Middlewares;
using CTBS.API.Models;
using CTBS.API.Movies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddEndpointsApiExplorer()
    .AddMovieServices()
    .AddSwaggerGen()
    .AddDbContext<CTBSDbContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();

    // using var scope = app.Services.CreateScope();
    // await using var dbContext = scope.ServiceProvider.GetRequiredService<CTBSDbContext>();
    // await dbContext.Database.MigrateAsync();
}

app.MapMoviesEndpoints();

app.Run();

public partial class Program
{
}