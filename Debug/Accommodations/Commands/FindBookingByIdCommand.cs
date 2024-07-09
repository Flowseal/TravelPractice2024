using Accommodations.Models;

namespace Accommodations.Commands;

public class FindBookingByIdCommand( IBookingService bookingService, Guid bookingId ) : ICommand
{
    public void Execute()
    {
        Booking? booking = bookingService.FindBookingById( bookingId );
        Console.WriteLine( booking != null
            ? $"Booking found: {booking.RoomCategory.Name} for User {booking.UserId}" // FIX: исправлен вывод названия категории
            : "Booking not found." );
    }

    public void Undo()
    {
        Console.WriteLine( $"Undo operation is not supported for {nameof( GetType )}." );
    }
}
