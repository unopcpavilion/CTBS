using CTBS.API.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CTBS.API.Models;

public class CTBSDbContext: DbContext
{
    public CTBSDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SetupProductsModel();
    }
    
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<Theater> Theaters { get; set; }
    
    public DbSet<Showtime> Showtimes { get; set; }
    
    public DbSet<Reservation> Reservations { get; set; }
    
    public DbSet<Booking> Bookings { get; set; }
    
    public DbSet<Seat> Seats { get; set; }
}

public class CTBSDBContextFactory: IDesignTimeDbContextFactory<CTBSDbContext>
{
    public CTBSDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CTBSDbContext>();

        if (optionsBuilder.IsConfigured)
        {
            return new CTBSDbContext(optionsBuilder.Options);
        }

        var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

        var connectionString =
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build()
                .GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlite(connectionString);

        return new CTBSDbContext(optionsBuilder.Options);
    }

    public static CTBSDbContext Create() => new CTBSDBContextFactory().CreateDbContext();
}
