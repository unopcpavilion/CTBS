using CTBS.API.Core;
using CTBS.API.Core.Commands;
using CTBS.API.Core.Exceptions;
using CTBS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.API.Movies;

internal class HandleRemoveMovie : ICommandHandler<RemoveMovie>
{
    private readonly IQueryable<Movie> _movies;
    private readonly Func<Movie, CancellationToken, ValueTask> _removeMovie;

    public HandleRemoveMovie(
        IQueryable<Movie> movies,
        Func<Movie, CancellationToken, ValueTask> removeMovie)
    {
        _movies = movies;
        _removeMovie = removeMovie;
    }

    public async ValueTask Handle(RemoveMovie command, CancellationToken ct)
    {
        var movie = await _movies.SingleOrDefaultAsync(p => p.Id == command.Id, ct);
        if (movie == null)
        {
            throw new EntityNotFoundException($"Movie with id {command.Id} doesn't exist");
        }

        await _removeMovie(movie, ct);
    }
}

public record RemoveMovie(Guid Id)
{
    public static RemoveMovie With(Guid? movieId) => new(movieId.AssertNotEmpty(nameof(movieId)));
}