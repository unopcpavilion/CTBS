using CTBS.API.Core.Queries;
using CTBS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.API.Movies;

public class HandleGetMovies : IQueryHandler<GetMovies, IReadOnlyList<MoviesListItem>>
{
    private readonly IQueryable<Movie> _movies;

    public HandleGetMovies(IQueryable<Movie> movies)
    {
        this._movies = movies;
    }

    public async ValueTask<IReadOnlyList<MoviesListItem>> Handle(GetMovies query, CancellationToken ct)
    {
        var (filter, genres, page, pageSize) = query;

        var filteredMovies = _movies;

        if (!string.IsNullOrEmpty(filter))
        {
            filteredMovies = filteredMovies
                .Where(movie =>
                    movie.Title.Contains(query.Filter!) ||
                    movie.Description.Contains(query.Filter!)
                );
        }

        if (genres != null && genres.Any())
        {
            filteredMovies = filteredMovies.Where(movie => genres.Contains(movie.Genre));
        }

        return await filteredMovies
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Select(p => new MoviesListItem(p.Id, p.Title))
            .ToListAsync(ct);
    }
}

public record GetMovies(
    string? Filter,
    Genre[]? Genres,
    int Page,
    int PageSize)
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;

    public static GetMovies With(string? filter, Genre[]? genres, int? page, int? pageSize)
    {
        page ??= DefaultPage;
        pageSize ??= DefaultPageSize;

        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page));
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

        return new GetMovies(filter, genres, page.Value, pageSize.Value);
    }
}

public record MoviesListItem(
    Guid Id,
    string Name
);