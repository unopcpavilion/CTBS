using CTBS.API.Core;
using CTBS.API.Core.Commands;
using CTBS.API.Core.Queries;
using CTBS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.API.Movies;

public static class Configuration
{
    public static IServiceCollection AddMovieServices(this IServiceCollection services) => services
        .AddQueryable<Movie, CTBSDbContext>()
        .AddQueryHandler<GetMovies, IReadOnlyList<MoviesListItem>, HandleGetMovies>()
        .AddQueryHandler<GetMovieDetails, MovieDetails?, HandleGetMovieDetails>()
        .AddCommandHandler<AddMovie, HandleAddMovie>(s =>
        {
            var dbContext = s.GetRequiredService<CTBSDbContext>();
            return new HandleAddMovie(dbContext.AddAndSave);
        })
        .AddCommandHandler<UpdateMovie, HandleUpdateMovie>(s =>
        {
            var dbContext = s.GetRequiredService<CTBSDbContext>();
            return new HandleUpdateMovie(dbContext.UpdateAndSave);
        });

    public static void SetupProductsModel(this ModelBuilder modelBuilder) => modelBuilder.Entity<Movie>();
}