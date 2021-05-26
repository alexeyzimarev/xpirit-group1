using System;
using System.Threading.Tasks;
using Eventuous;
using Hotel.Bookings.Domain;
using Hotel.Bookings.Domain.Bookings;

namespace Hotel.Bookings.Application.Bookings {
    public class BookingsCommandService : ApplicationService<Booking, BookingState, BookingId> {
        public BookingsCommandService(IAggregateStore store) : base(store) {
            OnNewAsync<BookingCommands.BookRoom>(
                cmd => new BookingId(cmd.BookingId),
                (booking, cmd, _) => {
                    return booking.BookRoom(
                        new BookingId(cmd.BookingId),
                        cmd.GuestId,
                        new RoomId(cmd.RoomId),
                        new StayPeriod(cmd.CheckInDate, cmd.CheckOutDate),
                        new Money(cmd.BookingPrice, cmd.Currency),
                        DateTimeOffset.Now,
                        (id, period) => new ValueTask<bool>(true)
                    );
                }
            );
        }
    }
}