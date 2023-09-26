using CTBS.API.Core;
using CTBS.API.Core.Queries;
using CTBS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.API.Movies;

internal class HandleGetMovieDetails: IQueryHandler<GetMovieDetails, MovieDetails?>
{
    private readonly IQueryable<Movie> _movies;

    public HandleGetMovieDetails(IQueryable<Movie> movies)
    {
        this._movies = movies;
    }

    public async ValueTask<MovieDetails?> Handle(GetMovieDetails query, CancellationToken ct)
    {
        var movie = await _movies.SingleOrDefaultAsync(p => p.Id == query.MovieId, ct);

        if (movie == null)
        {
            return null;
        }

        return new MovieDetails(
            movie.Id,
            movie.Genre,
            movie.Title,
            movie.Description,
            movie.Duration
        );
    }
}

public record GetMovieDetails(Guid MovieId)
{
    public static GetMovieDetails With(Guid? movieId) => new(movieId.AssertNotEmpty(nameof(movieId)));
}

public record MovieDetails(
    Guid Id,
    Genre Genre,
    string Name,
    string? Description,
    int Duration
);