using Eventuous;

namespace Hotel.Bookings.Domain.Bookings {
    public record BookingState : AggregateState<BookingState, BookingId> {
        public string     GuestId     { get; set; }
        public RoomId     RoomId      { get; set; }
        public StayPeriod Period      { get; set; }
        public Money      Price       { get; set; }
        public Money      Outstanding { get; set; }
        public bool       Paid        { get; set; }
    }
}
