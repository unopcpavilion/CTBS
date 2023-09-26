using System.Net;
using CTBS.API.Core.Commands;
using CTBS.API.Core.Queries;
using CTBS.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Movies;

public static class MoviesEndpoints
{
    public static void MapMoviesEndpoints(this WebApplication app)
    {
        app.MapGet(
                "/api/movies", async ([FromServices] QueryHandler<GetMovies, IReadOnlyList<MoviesListItem>> getProducts,
                        string? filter,
                        Genre[]? genres,
                        int? page,
                        int? pageSize,
                        CancellationToken ct) => 
                   await getProducts(GetMovies.With(filter, genres, page, pageSize), ct)
            )
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Provide a list of movie");
        
        app.MapGet(
                "/api/movies/{id:guid}",
                async (
                        [FromServices] QueryHandler<GetMovieDetails, MovieDetails?> getMovieById,
                        Guid id,
                        CancellationToken ct
                    ) =>
                    await getMovieById(GetMovieDetails.With(id), ct) is { } movie
                        ? Results.Ok(movie)
                        : Results.NotFound()
            )
            .Produces<MovieDetails>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get movie details by Id");
        
        app.MapPut(
                "/api/movies",
                async (
                    [FromServices] CommandHandler<UpdateMovie> updateMove,
                    UpdateMovieRequest request,
                    CancellationToken ct
                ) =>
                {
                    var (id, duration, description, title, genre) = request;
        
                    await updateMove(UpdateMovie.With(id, duration, description, title, genre), ct);
        
                    return Results.Ok();
                }
            )
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get movie details by Id");
        
        
        // Add New Movie
        app.MapPost(
                "api/movies/",
                async (
                    [FromServices] CommandHandler<AddMovie> addMovie,
                    AddMovieRequest request,
                    CancellationToken ct
                ) =>
                {
                    var movieId = Guid.NewGuid();
                    var (duration, description, title, genre) = request;
        
                    await addMovie(AddMovie.With(movieId, duration, description, title, genre), ct);
        
                    return Results.Created($"/api/movies/{movieId}", movieId);
                }
            )
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Add new movie");
        
        app.MapDelete(
                "api/movies/{id:guid}",
                async (
                    [FromServices] CommandHandler<RemoveMovie> removeMove,
                    Guid id,
                    CancellationToken ct
                ) =>
                {
                    await removeMove(RemoveMovie.With(id), ct);
        
                    return Results.Ok();
                }
            )
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Remove movie by Id");
    }
}