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
            Money           prepaid,
            DateTimeOffset  bookedAt,
            IsRoomAvailable isRoomAvailable
        ) {
            EnsureDoesntExist();
            await EnsureRoomAvailable(roomId, period, isRoomAvailable);

            var outstanding = price - prepaid;

            Apply(
                new BookingEvents.RoomBooked(
                    bookingId,
                    guestId,
                    roomId,
                    period.CheckIn,
                    period.CheckOut,
                    price.Amount,
                    prepaid.Amount,
                    outstanding.Amount,
                    price.Currency,
                    outstanding.Amount == 0,
                    bookedAt
                )
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

        protected override BookingState When(object evt) {
            return evt switch {
                BookingEvents.RoomBooked booked => State with {
                    Id = booked.BookingId,
                    RoomId = new RoomId(booked.RoomId),
                    Period = new StayPeriod(booked.CheckInDate, booked.CheckOutDate),
                    GuestId = booked.GuestId,
                    Price = new Money(booked.BookingPrice, booked.Currency),
                    Outstanding = new Money(booked.OutstandingAmount, booked.Currency),
                    Paid = booked.Paid
                },
                _ => State
            };
        }
    }
}