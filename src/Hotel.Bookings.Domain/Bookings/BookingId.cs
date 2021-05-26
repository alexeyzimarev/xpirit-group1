using Eventuous;

namespace Hotel.Bookings.Domain.Bookings {
    public record BookingId(string Value) : AggregateId(Value);
}
