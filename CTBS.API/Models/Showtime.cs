using CTBS.API.Movies;

namespace CTBS.API.Models;

public class Showtime
{
    public int Id { get; set; }
    
    public DateTime Start { get; set; }
    
    public Guid MovieId { get; set; }
    
    public Movie Movie { get; set; }
    
    public int TheaterId { get; set; }
    
    public Theater Theater { get; } 
}