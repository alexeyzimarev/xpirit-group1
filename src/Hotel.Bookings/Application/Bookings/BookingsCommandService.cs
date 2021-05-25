using CoreLib;
using Hotel.Bookings.Domain;
using Hotel.Bookings.Domain.Bookings;
using static Hotel.Bookings.Domain.Services;

namespace Hotel.Bookings.Application.Bookings {
    public class BookingsCommandService : CommandService<Booking, BookingId, BookingState> {
        public BookingsCommandService(IAggregateStore store) : base(store) {
        }
    }
}
