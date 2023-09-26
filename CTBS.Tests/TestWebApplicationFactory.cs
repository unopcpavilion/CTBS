using CTBS.API.Models;

namespace CTBS.Tests;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

public class TestWebApplicationFactory: WebApplicationFactory<Program>
{
    private readonly string schemaName = Guid.NewGuid().ToString("N").ToLower();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services
                .AddTransient(s =>
                {
                    var connectionString = s.GetRequiredService<IConfiguration>()
                        .GetConnectionString("WarehouseDB");
                    var options = new DbContextOptionsBuilder<CTBSDbContext>();
                    options.UseSqlite($"{connectionString}");
                    return options.Options;
                });
        });

        var host = base.CreateHost(builder);

        return host;
    }
}