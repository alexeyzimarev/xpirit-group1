using System;
using System.Threading.Tasks;
using CoreLib;
using static Hotel.Bookings.Domain.Services;

namespace Hotel.Bookings.Domain.Bookings {
    public class Booking : Aggregate<BookingId, BookingState> {
        public Booking() => State = new BookingState();

        public async Task BookRoom(
            BookingId       bookingId,
            string          guestId,
            RoomId          roomId,
            StayPeriod      period,
            Money           price,
            DateTimeOffset  bookedAt,
            IsRoomAvailable isRoomAvailable
        ) {
            EnsureDoesntExist();
            await EnsureRoomAvailable(roomId, period, isRoomAvailable);

            ChangeState(
                State with {
                    Id = bookingId,
                    GuestId = guestId,
                    RoomId = roomId,
                    Price = price,
                    Period = period,
                    Outstanding = price,
                    Paid = price == 0
                }
            );
        }

        public void RecordPayment(
            Money           paid,
            ConvertCurrency convertCurrency,
            string          paidBy,
            DateTimeOffset  paidAt
        ) {
            EnsureExists();
        }

        static async Task EnsureRoomAvailable(RoomId roomId, StayPeriod period, IsRoomAvailable isRoomAvailable) {
            var roomAvailable = await isRoomAvailable(roomId, period);
            if (!roomAvailable) throw new DomainException("Room not available");
        }
    }
}