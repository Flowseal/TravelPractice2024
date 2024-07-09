namespace Accommodations.Models;

public class Booking
{
    public Guid Id { get; init; }
    public int UserId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public RoomCategory RoomCategory { get; init; }
    public Currency Currency { get; init; }
    public decimal Cost { get; init; }
}
