using Eventuous;

namespace Hotel.Bookings.Domain.Bookings {
    public record BookingState : AggregateState<BookingState, BookingId> {
        public string     GuestId     { get; init; }
        public RoomId     RoomId      { get; init; }
        public StayPeriod Period      { get; init; }
        public Money      Price       { get; init; }
        public Money      Outstanding { get; init; }
        public bool       Paid        { get; init; }

        public override BookingState When(object evt) {
            return evt switch {
                BookingEvents.RoomBooked booked => this with {
                    Id = new BookingId(booked.BookingId),
                    RoomId = new RoomId(booked.RoomId),
                    Period = new StayPeriod(booked.CheckInDate, booked.CheckOutDate),
                    GuestId = booked.GuestId,
                    Price = new Money(booked.BookingPrice, booked.Currency),
                    Outstanding = new Money(booked.OutstandingAmount, booked.Currency),
                    Paid = booked.Paid
                },
                _ => this
            };
        }
    }
}
