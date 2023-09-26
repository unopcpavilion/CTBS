using CTBS.API.Core.Commands;
using CTBS.API.Models;

namespace CTBS.API.Movies;

public class HandleAddMovie : ICommandHandler<AddMovie>
{
    private readonly Func<Movie, CancellationToken, ValueTask> _addMovie;

    public HandleAddMovie(Func<Movie, CancellationToken, ValueTask> addMovie)
    {
        this._addMovie = addMovie;
    }

    public async ValueTask Handle(AddMovie command, CancellationToken ct)
    {
        var product = new Movie
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            Duration = command.Duration,
            Genre = command.Genre
        };
        
        await _addMovie(product, ct);
    }
}

public record AddMovie(
    Guid Id, 
    int Duration,
    string Title,
    string Description,
    Genre Genre
)
{
    public static AddMovie With(Guid? id, int? duration, string? description, string? title, Genre? genre)
    {
        if (!id.HasValue || id == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(id));
        if (genre is null) throw new ArgumentOutOfRangeException(nameof(genre));
        if (string.IsNullOrEmpty(title)) throw new ArgumentOutOfRangeException(nameof(title));
        if (string.IsNullOrEmpty(description)) throw new ArgumentOutOfRangeException(nameof(description));
        if (duration is null or <= 0) throw new ArgumentOutOfRangeException(nameof(duration));

        return new AddMovie(id.Value, duration.Value, title, description, genre.Value);
    }
}

public record AddMovieRequest(
    int? Duration, 
    string? Description, 
    string? Title, 
    Genre? Genre);