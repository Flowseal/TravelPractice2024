namespace Accommodations.Models;

public class RoomCategory
{
    public string Name { get; init; }
    public decimal BaseRate { get; init; }
    public int AvailableRooms { get; set; }
}
