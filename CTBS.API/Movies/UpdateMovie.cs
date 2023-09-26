using CTBS.API.Core.Commands;

namespace CTBS.API.Movies;


internal class HandleUpdateMovie : ICommandHandler<UpdateMovie>
{
    private readonly Func<Movie, CancellationToken, ValueTask> _updateMovie;

    public HandleUpdateMovie(Func<Movie, CancellationToken, ValueTask> updateMovie)
    {
        this._updateMovie = updateMovie;
    }

    public async ValueTask Handle(UpdateMovie command, CancellationToken ct)
    {
        var product = new Movie
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            Duration = command.Duration,
            Genre = command.Genre
        };
        
        await _updateMovie(product, ct);
    }
}

public record UpdateMovie( 
    Guid Id, 
    int Duration,
    string Title,
    string Description,
    Genre Genre)
{
    public static UpdateMovie With(Guid? id, int? duration, string? description, string? title, Genre? genre)
    {
        if (!id.HasValue || id == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(id));
        if (genre is null) throw new ArgumentOutOfRangeException(nameof(genre));
        if (string.IsNullOrEmpty(title)) throw new ArgumentOutOfRangeException(nameof(title));
        if (string.IsNullOrEmpty(description)) throw new ArgumentOutOfRangeException(nameof(description));
        if (duration is null or <= 0) throw new ArgumentOutOfRangeException(nameof(duration));

        return new UpdateMovie(id.Value, duration.Value, title, description, genre.Value);
    }
}

public record UpdateMovieRequest(
    Guid? Id,
    int? Duration, 
    string? Description, 
    string? Title, 
    Genre? Genre);