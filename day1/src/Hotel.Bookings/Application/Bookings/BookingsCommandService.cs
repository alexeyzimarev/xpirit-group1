using System;
using System.Threading.Tasks;
using CoreLib;
using Hotel.Bookings.Domain;
using Hotel.Bookings.Domain.Bookings;

namespace Hotel.Bookings.Application.Bookings {
    public class BookingsCommandService : CommandService<Booking, BookingId, BookingState> {
        public BookingsCommandService(IAggregateStore store) : base(store) {
            OnNew<BookingCommands.BookRoom>(
                (booking, cmd) => {
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