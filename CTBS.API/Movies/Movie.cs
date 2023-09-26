namespace CTBS.API.Movies;

/// <summary>
/// Represents a Movie as entity
/// </summary>
public class Movie 
{
    /// <summary>
    /// An unique identifier for the movie
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The title of the movie
    /// </summary>
    public string Title { get; set; } = default!;
    
    /// <summary>
    /// A short description about the movie
    /// </summary>
    public string Description { get; set; } = default!;
    
    /// <summary>
    /// The duration of the movie in minutes
    /// </summary>
    public int Duration { get; set; }
    
    /// <summary>
    /// The genre of the movie
    /// </summary>
    public Genre Genre { get; set; }
}