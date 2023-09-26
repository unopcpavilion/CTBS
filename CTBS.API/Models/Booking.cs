namespace CTBS.API.Models;

public class Booking
{
    public int Id { get; set; }
    
    public int ReservationId { get; set; }
    
    public Reservation Reservation { get; set; }
    
    public decimal TotalPrice { get; set; }
}