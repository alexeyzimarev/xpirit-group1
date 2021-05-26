using System;

namespace Hotel.Bookings.Domain.Bookings {
    public static class BookingEvents {
        public record RoomBooked(
            string         BookingId,
            string         GuestId,
            string         RoomId,
            DateTimeOffset CheckInDate,
            DateTimeOffset CheckOutDate,
            float          BookingPrice,
            float          PrepaidAmount,
            float          OutstandingAmount,
            string         Currency,
            bool           Paid,
            DateTimeOffset BookingDate
        );
    }
}