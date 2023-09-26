namespace CTBS.API.Models;

public class Theater
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public ICollection<Seat> Seats { get; set; }
}

public class Seat
{
    public int Id { get; set; }

    public int TheaterId { get; set; }

    public virtual Theater Theater { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public bool IsAvailable { get; set; }
}

// Seats Arrangement:
// [ 1,  0,  0,  0,  2 ]
// [ 3,  4,  5,  6,  7 ]
// [ 8,  9,  10, 11, 12]
// [ 0,  0,  13, 0,  0 ]
// Count: 13
