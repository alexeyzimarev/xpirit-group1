using Eventuous;

namespace Hotel.Bookings.Domain {
    public record RoomId(string Value) : AggregateId(Value);
}
