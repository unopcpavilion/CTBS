namespace CTBS.API.Models;

public class Reservation
{
    public int Id { get; set; }
    
    public int ShowtimeId { get; set; }
    
    public Showtime Showtime { get; set; }
    
    public DateTime ReservedUntil { get; set; }
    
    public List<Seat> ReservedSeats { get; set; }
}