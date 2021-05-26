using System;
using Eventuous.Projections.MongoDB.Tools;

namespace Hotel.Bookings.Application.Projections {
    public record BookingDocument : ProjectedDocument {
        public BookingDocument(string id) : base(id) { }

        public string         GuestId      { get; init; }
        public float          Price        { get; init; }
        public float          Outstanding  { get; init; }
        public bool           FullyPaid    { get; init; }
        public DateTimeOffset CheckInDate  { get; init; }
        public DateTimeOffset CheckOutDate { get; init; }
    }
}