using System;

namespace Hotel.Bookings.Application.Bookings {
    public static class BookingCommands {
        public record BookRoom(
            string         BookingId,
            string         GuestId,
            string         RoomId,
            DateTimeOffset CheckInDate,
            DateTimeOffset CheckOutDate,
            float          BookingPrice,
            float          PrepaidAmount,
            string         Currency,
            DateTimeOffset BookingDate
        );
    }
}