using Accommodations.Models;

namespace Accommodations;

public interface IBookingService
{
    Booking? Book( int userId, string categoryName, DateTime startDate, DateTime endDate, Currency currency );
    void CancelBooking( Guid bookingId );
    Booking? FindBookingById( Guid bookingId );
    IEnumerable<Booking?> SearchBookings( DateTime startDate, DateTime endDate, string categoryName );
    decimal CalculateCancellationPenaltyAmount( Booking booking );
}
