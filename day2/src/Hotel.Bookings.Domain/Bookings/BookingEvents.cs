using System;
using Eventuous;

namespace Hotel.Bookings.Domain.Bookings {
    public static class BookingEvents {
        public static class V1 {
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
                DateTimeOffset BookingDate
            );

            public record PaymentRecorded(string BookingId, float AmountPaid, string Currency, string PaymentId);
            
            public record BookingFullyPaid(string BookingId);
        }

        public static class V2 {
            public record PaymentRecorded(string BookingId, float AmountPaid, float Outstanding, string Currency, string PaymentId);
        }

        public static void MapEvents() {
            TypeMap.AddType<V1.RoomBooked>("V1.RoomBooked");
            TypeMap.AddType<V1.PaymentRecorded>("V1.PaymentRecorded");
            TypeMap.AddType<V2.PaymentRecorded>("V2.PaymentRecorded");
            TypeMap.AddType<V1.BookingFullyPaid>("V1.BookingFullyPaid");
        }
    }
}